using AdobeConnectDownloader.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdobeConnectDownloader.Application
{
    public class FFMPEGManager
    {
        public Process? CurrentProcess { get; set; }
        public void RunProcess(ProcessStartInfo processStartInfo, string command)
        {
            Process? ffmpegProcess;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.Arguments = command;
            ffmpegProcess = Process.Start(processStartInfo);
            CurrentProcess = ffmpegProcess;
            ffmpegProcess?.WaitForExit();
        }

        public void RunMultiProcess(ProcessStartInfo processStartInfo, List<string> commands)
        {
            foreach (var command in commands)
            {
                RunProcess(processStartInfo, command);
            }
        }

        public static string GetMergeAudioCommand(List<StreamData> streamDatas, string dataFolderPath, string outputFileAddress)
        {
            string Command = "-hide_banner ";
            string adelayStr = $"[0:a]adelay={streamDatas[0].StartFilesTime}[a0];";
            string amixInput = "[a0]";

            Command += $" -i \"{Path.Combine(dataFolderPath, streamDatas[0].FileNames + ".flv")}\"";
            if (streamDatas.Count > 1)
            {
                for (int i = 1; i < streamDatas.Count; i++)
                {
                    Command += $" -i \"{Path.Combine(dataFolderPath, streamDatas[i].FileNames + ".flv")}\"";
                    adelayStr += $"[{i}:a]adelay={streamDatas[i].StartFilesTime}[a{i}];";
                    amixInput += $"[a{i}]";
                }
            }
            amixInput += $"amix=inputs={streamDatas.Count}[out]";

            Command += $" -filter_complex \"{adelayStr}{amixInput}\" -map \"[out]\" -y \"{outputFileAddress}\"";

            return Command;
        }

        public static string GetCommandForCreateEmptyVideo(uint milieSecond, string folderPathForCreateFile, string size, string imageAddress, string outputAddress)
        {

            string imageAdressCopy = imageAddress.Replace("\\", "\\\\");
            string duration = Helper.Time.ConvertUintToDuration(milieSecond);
            var emptyConcatAddress = CreateConcatForEmptyVideo(folderPathForCreateFile, duration, imageAdressCopy);

            string command = $"-hide_banner -safe 0 -f concat -i \"{emptyConcatAddress}\" -r 1 -crf 22 -threads 2 -vf scale={size.Replace('x', ':') } -preset veryfast -y \"{outputAddress}\"";

            return command;
        }

        private static string CreateConcatForEmptyVideo(string folderPathForCreateFile, string duration, string imageAddress)
        {
            string fileAddress = Path.Combine(folderPathForCreateFile, "ConcatTextFile.txt");
            string concatData = $"file \'{imageAddress}\'{Environment.NewLine}" +
                $"duration {duration + Environment.NewLine}" +
                $"file \'{imageAddress}\'";

            if (File.Exists(fileAddress))
            {
                File.WriteAllText(fileAddress, concatData);
            }
            else
            {
                var x = File.Create(fileAddress);
                x.Close();
                File.WriteAllText(fileAddress, concatData);
            }

            return fileAddress;
        }

        public static string CreateConcatFile(List<string> listOfFilePath, string outputAddress)
        {
            string command = "-hide_banner ";
            string filter_Complex = "-filter_complex \"";
            for (int i = 0; i < listOfFilePath.Count; i++)
            {
                command += $"-i \"{listOfFilePath[i]}\" ";
                filter_Complex += $"[{i}:v] ";
            }

            filter_Complex += $"concat=n={listOfFilePath.Count}:v=1[v]\" -map \"[v]\" -y \"{outputAddress}\"";
            command += filter_Complex;

            return command;
        }

        public static string GetVideosResolotion(string videoAddress, string ffmpegAddress)
        {

            var command = $"-hide_banner -i \"{videoAddress}\"";

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
            var index1 = data.IndexOf("Stream #0:1: Video:");
            int index2 = data.IndexOf('x', index1);
            int index3 = data.IndexOf(',', index2);
            int index4 = data.IndexOf(',', index2 - 8);

            string Size = data.Substring(index4, index3 - index4).Replace(',', ' ').Trim();
            process.WaitForExit();

            return Size;
        }

        public static string ConvertFlvVideoToMp4(string videoAddress, string outputAddress)
        {
            string command = $"-hide_banner -i \"{videoAddress}\" -y -preset ultrafast -vf fps=30 -codec libx264 -c:a libmp3lame -quality good \"{outputAddress}\"";
            return command;
        }

        public static string ConvertFlvAudioToMp3(string audioAddress, string output)
        {
            string command = $"-hide_banner -i \"{audioAddress}\" -y -c:a libmp3lame -b:a 320k -preset ultrafast \"{output}\"";
            return command;
        }

    }
}
