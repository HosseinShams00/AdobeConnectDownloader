using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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

        public static void CheckHealthyFiles(ListOfStreamData listOfStreamData, string extractedDataFolder, string ffmpegAddress)
        {
            var namesOfCorrectFiles = new List<StreamData>();

            foreach (var data in listOfStreamData.AudioStreamData)
            {
                var healthy = IsFileHealthy(Path.Combine(extractedDataFolder, data.FileNames + data.Extension), ffmpegAddress);
                if (healthy == false)
                    namesOfCorrectFiles.Add(data);
            }

            if (namesOfCorrectFiles.Count != 0)
            {
                foreach (var item in namesOfCorrectFiles)
                {
                    listOfStreamData.AudioStreamData.Remove(item);
                }
            }

            namesOfCorrectFiles = new List<StreamData>();


            foreach (var data in listOfStreamData.ScreenStreamData)
            {
                var healthy = IsFileHealthy(Path.Combine(extractedDataFolder, data.FileNames + data.Extension), ffmpegAddress);
                if (healthy == false)
                    namesOfCorrectFiles.Add(data);
            }

            if (namesOfCorrectFiles.Count == 0)
                return;

            foreach (var item in namesOfCorrectFiles)
            {
                listOfStreamData.ScreenStreamData.Remove(item);
            }


        }

        private static bool IsFileHealthy(string fileAddress, string ffmpegAddress)
        {
            var res = false;
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
                res = false;
            else
                res = true;

            process.WaitForExit();
            return res;
        }

    }
}
