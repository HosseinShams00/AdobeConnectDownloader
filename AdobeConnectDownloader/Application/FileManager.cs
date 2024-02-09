using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
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

            using (ZipArchive zip = ZipFile.OpenRead(zipAddress))
            {
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
            }

            return filesAddress;
        }

        public static ListOfStreamData CheckFiles(List<StreamData> streamDatas, string extractedDataFolder, string ffmpegAddress)
        {
            var result = new ListOfStreamData();

            foreach (var data in streamDatas)
            {
                var contentType = CheckFileContentType(Path.Combine(extractedDataFolder, data.FileNames + data.Extension), ffmpegAddress);
                data.StreamContentTypeEnum = contentType;
                switch (contentType)
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

        private static StreamContentTypeEnum CheckFileContentType(string fileAddress, string ffmpegAddress)
        {
            var res = StreamContentTypeEnum.Empty;
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
            var data = process.StandardError.ReadToEnd();
            var index1 = data.IndexOf("Duration:");
            var audioIndex = data.IndexOf("Audio", index1);
            var videoIndex = data.IndexOf("Video", index1);

            if (audioIndex == -1 && videoIndex == -1)
            {
                res = StreamContentTypeEnum.Empty;
            }
            else if (audioIndex != -1 && videoIndex != -1)
            {
                res = StreamContentTypeEnum.VideoWithAudio;
            }
            else if (audioIndex == -1 && videoIndex != -1)
            {
                res = StreamContentTypeEnum.Video;
            }
            else if (audioIndex != -1 && videoIndex == -1)
            {
                res = StreamContentTypeEnum.Audio;
            }

            process.WaitForExit();
            return res;
        }

    }
}
