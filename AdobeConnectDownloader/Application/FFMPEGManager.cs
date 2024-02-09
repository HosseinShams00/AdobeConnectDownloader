using AdobeConnectDownloader.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

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
            //string duration = Helper.Time.ConvertUintToDuration(milieSecond); // old Code
            //string duration = Helper.Time.ConvertUintToDurationV2(milieSecond);
            string command = $"-hide_banner -loop 1 -framerate 2 -i \"{imageAddress}\" -y -t {milieSecond}ms -ss \"00:00:00\" -y \"{outputAddress}\" ";

            return command;
        }

        public static string GetCommandForCreateEmptyVideoV2(string videoAddress, uint timeInMilieSecond, string outputAddress)
        {
            string command = $"-hide_banner -i \"{videoAddress}\" -vf \"tpad=start_duration={timeInMilieSecond}ms\" -strict experimental -y \"{outputAddress}\"";
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
            process.WaitForExit();
            var getResolutionRegex = new Regex("([0-9]{3,}x[0-9]+)");
            var resolution = getResolutionRegex.Match(data);
            var size = string.Empty;
            if (string.IsNullOrEmpty(resolution.Value) == false)
            {
                size = resolution.Value;
            }

            return size;
        }

        public static void FixVideoBug(StreamData data, string extractFolder, int originalWidth, int originalHeight, string videoOutput, string ffmpegAddress)
        {
            var videoAddress = Path.Combine(extractFolder, data.FileNames + data.Extension);

            var scaleCommand = string.Empty;

            if ((data.Width == originalWidth && data.Height == originalHeight) == false)
            {
                scaleCommand = $"-vf \"scale={originalWidth}:{originalHeight}\"";
                data.Width = originalWidth;
                data.Height = originalHeight;
            }
            else
            {
                scaleCommand = " -c:v copy";
            }

            string command = $"-hide_banner -i \"{videoAddress}\" -ss \"00:00:00\" {scaleCommand} -preset ultrafast -y \"{videoOutput}\"";

            var processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = ffmpegAddress;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.Arguments = command;
            var ffmpegProcess = Process.Start(processStartInfo);
            ffmpegProcess?.WaitForExit();

        }

        public static string GetVideoTime(string videoAddress, string ffmpegAddress)
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
            var durationStart = data.IndexOf("Duration: ") + "Duration: ".Length;
            var durationEnd = data.IndexOf(',', durationStart);
            var res = data[durationStart..durationEnd];
            process.WaitForExit();
            return res;
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
