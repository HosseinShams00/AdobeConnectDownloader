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

        public List<string> ExtractZipFile(string zipFileAddress, string extractFolder)
        {
            List<string> filesAddress = new List<string>();

            using (ZipArchive zip = ZipFile.OpenRead(zipFileAddress))
            {
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
                    else if (item.FullName == "indexstream.xml")
                    {
                        string ExtractedFileAddress = Path.Combine(extractFolder, item.FullName);
                        item.ExtractToFile(ExtractedFileAddress, true);
                        filesAddress.Add(ExtractedFileAddress);
                    }
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
            List<StreamData> namesOfCorrectFiles = new List<StreamData>();

            for (int i = 0; i < listOfStreamData.AudioStreamData.Count; i++)
            {
                bool healthy = IsFileHealthy(Path.Combine(extractedDataFolder, listOfStreamData.AudioStreamData[i].FileNames + ".flv"), ffmpegAddress);
                if (healthy == false)
                    namesOfCorrectFiles.Add(listOfStreamData.AudioStreamData[i]);
            }

            if (namesOfCorrectFiles.Count != 0)
            {
                foreach (var item in namesOfCorrectFiles)
                {
                    listOfStreamData.AudioStreamData.Remove(item);
                }
            }

            namesOfCorrectFiles = new List<StreamData>();


            for (int i = 0; i < listOfStreamData.ScreenStreamData.Count; i++)
            {
                bool healthy = IsFileHealthy(Path.Combine(extractedDataFolder, listOfStreamData.ScreenStreamData[i].FileNames + ".flv"), ffmpegAddress);
                if (healthy == false)
                    namesOfCorrectFiles.Add(listOfStreamData.ScreenStreamData[i]);
            }

            if (namesOfCorrectFiles.Count != 0)
            {
                foreach (var item in namesOfCorrectFiles)
                {
                    listOfStreamData.ScreenStreamData.Remove(item);
                }
            }


        }

        private static bool IsFileHealthy(string fileAddress, string ffmpegAddress)
        {
            bool res = false;
            var command = $"-hide_banner -i \"{fileAddress}\"";

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = ffmpegAddress;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.Arguments = command;
            processStartInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            string data = process.StandardError.ReadToEnd();
            var index1 = data.IndexOf("Duration:");
            int audioIndex = data.IndexOf("Audio", index1);
            int videoIndex = data.IndexOf("Video", index1);

            if (audioIndex == -1 && videoIndex == -1)
                res = false;
            else
                res = true;

            process.WaitForExit();
            return res;
        }

    }
}
