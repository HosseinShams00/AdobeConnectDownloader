using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using AdobeConnectDownloader.Enums;
using AdobeConnectDownloader.Model;


namespace AdobeConnectDownloader.Application
{
    public class FileManager
    {
        public FFMPEGManager FFMPEGManager = new FFMPEGManager();

        public List<string> ExtractZipFile(string? zipFileAddress, string extractFolder)
        {
            var filesAddress = new List<string>();

            using ZipArchive zip = ZipFile.OpenRead(zipFileAddress);
            foreach (var item in zip.Entries)
            {
                if (item.FullName.EndsWith(".flv"))
                {
                    if (item.FullName.StartsWith("cameraVoip_") || item.FullName.StartsWith("screenshare_"))
                    {
                        string ExtractedFileAddress = Path.Combine(extractFolder, item.FullName);
                        item.ExtractToFile(ExtractedFileAddress, true);
                        filesAddress.Add(ExtractedFileAddress);
                    }
                }
                else if (item.FullName == "indexstream.xml" || item.FullName == "mainstream.xml")
                {
                    string ExtractedFileAddress = Path.Combine(extractFolder, item.FullName);
                    item.ExtractToFile(ExtractedFileAddress, true);
                    filesAddress.Add(ExtractedFileAddress);
                }
            }

            return filesAddress;
        }

        public List<string> GetZipFilesName(string zipAddress)
        {
            List<string> filesAddress = new List<string>();

            using ZipArchive zip = ZipFile.OpenRead(zipAddress);
            foreach (var item in zip.Entries)
            {
                if (item.FullName.EndsWith(".flv"))
                {
                    if (item.FullName.StartsWith("cameraVoip_") || item.FullName.StartsWith("screenshare_"))
                    {
                        filesAddress.Add(item.FullName);
                    }
                }
            }

            return filesAddress;
        }

        public static ListOfStreamData CheckFiles(List<StreamData> streamDatas, string extractedDataFolder, string ffmpegAddress)
        {
            var result = new ListOfStreamData();

            foreach (var data in streamDatas)
            {
                GetFileDetail(data, extractedDataFolder, ffmpegAddress);

                switch (data.StreamContentTypeEnum)
                {
                    case StreamContentTypeEnum.Audio:
                        {
                            result.AudioStreamData.Add(data);
                            break;
                        }
                    case StreamContentTypeEnum.Video:
                        {
                            if (data.FileNames.ToLower().StartsWith("camera"))
                            {
                                result.WebCamStreamData.Add(data);
                                break;
                            }

                            result.ScreenStreamData.Add(data);
                            break;
                        }
                    case StreamContentTypeEnum.VideoWithAudio:
                        {
                            result.AudioStreamData.Add(data);
                            result.ScreenStreamData.Add(data);
                            break;
                        }
                }
            }

            result.AudioStreamData = result.AudioStreamData.OrderBy(x => x.StartFilesTime).ToList();
            result.ScreenStreamData = result.ScreenStreamData.OrderBy(x => x.StartFilesTime).ToList();
            result.WebCamStreamData = result.WebCamStreamData.OrderBy(x => x.StartFilesTime).ToList();

            return result;

        }

        private static void GetFileDetail(StreamData data, string extractedDataFolder, string ffmpegAddress)
        {
            var fileAddress = Path.Combine(extractedDataFolder, data.FileNames + data.Extension);

            var command = $"-hide_banner -i \"{fileAddress}\"";

            var processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = ffmpegAddress;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.Arguments = command;
            processStartInfo.CreateNoWindow = true;
            var process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            var ffmpegResult = process.StandardError.ReadToEnd();
            process.WaitForExit();


            GetTypeOfStream(data, ffmpegResult);
            SetVideoResolution(data, ffmpegResult);
        }

        private static void SetVideoResolution(StreamData data, string ffmpegResult)
        {
            var getResolutionRegex = new Regex("([0-9]{3,}x[0-9]+)");
            var resolution = getResolutionRegex.Match(ffmpegResult);
            if (string.IsNullOrEmpty(resolution.Value) == false)
            {
                var sizeOfVideo = resolution.Value.Split("x");
                data.Width = int.Parse(sizeOfVideo[0]);
                data.Height = int.Parse(sizeOfVideo[1]);
            }
        }

        private static void GetTypeOfStream(StreamData data, string ffmpegResult)
        {
            var getTypeOfContentRegex = new Regex("Stream #.*?(Video|Audio):");
            var content = getTypeOfContentRegex.Matches(ffmpegResult);
            bool isAudio = content.Any(x => x.Value.ToLower().Contains("audio"));
            bool isVideo = content.Any(x => x.Value.ToLower().Contains("video"));

            if (isAudio == false && isVideo == false)
            {
                data.StreamContentTypeEnum = StreamContentTypeEnum.Empty;
            }
            else if (isAudio && isVideo)
            {
                data.StreamContentTypeEnum = StreamContentTypeEnum.VideoWithAudio;
            }
            else if (isAudio == false && isVideo)
            {
                data.StreamContentTypeEnum = StreamContentTypeEnum.Video;
            }
            else if (isAudio && isVideo == false)
            {
                data.StreamContentTypeEnum = StreamContentTypeEnum.Audio;
            }
        }
    }
}
