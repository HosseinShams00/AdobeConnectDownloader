using AdobeConnectDownloader.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdobeConnectDownloader.Application
{
    public class FFMPEGManager
    {
        public Process? CurrentProcess { get; set; }
        public void RunProcess(ProcessStartInfo processStartInfo)
        {
            Process? ffmpegProcess;
            processStartInfo.CreateNoWindow = true;
            ffmpegProcess = Process.Start(processStartInfo);
            CurrentProcess = ffmpegProcess;
            ffmpegProcess?.WaitForExit();
        }
        public string RunProcessAndGetOutput(ProcessStartInfo processStartInfo)
        {
            Process? ffmpegProcess;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardError = true;
            ffmpegProcess = Process.Start(processStartInfo);
            CurrentProcess = ffmpegProcess;
            string result = ffmpegProcess?.StandardError.ReadToEnd();
            ffmpegProcess?.WaitForExit();
            return result;
        }

        public void RunMultiProcess(ProcessStartInfo processStartInfo, List<string> commands)
        {
            foreach (var command in commands)
            {
                processStartInfo.Arguments = command;
                RunProcess(processStartInfo);
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

        public static string GetCommandForCreateEmptyVideo(uint milieSecond, string imageAddress, string outputAddress)
        {
            string duration = Helper.Time.ConvertUintToDuration(milieSecond);
            string command = $"-hide_banner -loop 1 -framerate 2 -i \"{imageAddress}\" -y -t {duration} \"{outputAddress}\" ";

            return command;
        }

        public static string CreateConcatFile(List<string> listOfFilePath, string outputAddress)
        {
            string command = $"-hide_banner ";
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


        public static string CreateConcatFile(List<string> listOfFilePath, string audioAddress, string outputAddress)
        {
            string command = $"-hide_banner -i \"{audioAddress}\" ";
            string filter_Complex = "-filter_complex \"";

            for (int i = 0; i < listOfFilePath.Count; i++)
            {
                command += $"-i \"{listOfFilePath[i]}\" ";
                filter_Complex += $"[{i + 1}:v] ";
            }

            filter_Complex += $"concat=n={listOfFilePath.Count}:v=1[v]\" -map \"[v]\" -map 0:a -c:a aac -y \"{outputAddress}\"";
            command += filter_Complex;

            return command;
        }

        public static string GetVideosResolution(string videoAddress, string ffmpegAddress)
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
            var index1 = data.IndexOf("Video:");
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

        public static string RemoveAudioFromVideoCommand(string videoAddress, string outputAddress)
        {
            return $"-hide_banner -i \"{videoAddress}\" -an -c:v copy -y \"{outputAddress}\"";
        }
    }
}
