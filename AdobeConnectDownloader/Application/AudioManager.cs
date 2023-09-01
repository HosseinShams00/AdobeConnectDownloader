using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AdobeConnectDownloader.Model;


namespace AdobeConnectDownloader.Application
{
    public class AudioManager
    {
        public FFMPEGManager FFMPEGManager { get; set; }

        public AudioManager(FFMPEGManager fFMPEGManager)
        {
            FFMPEGManager = fFMPEGManager;
        }


        public string MatchAllAudio(List<StreamData> audioStreamDatas, string dataFolderPath, string outputFileFolder, string ffmpegAddress)
        {
            var finalAudionPath = Path.Combine(outputFileFolder, "FinalAudio.flv");
            var copyOfAudioStreamDatas = audioStreamDatas;
            var audioLineData = AudioLine(copyOfAudioStreamDatas);
            var ffmpegCommands = new List<string>();

            int counter = 0;
            var streamDatas = new List<StreamData>();

            var firstAudioPath = Path.Combine(dataFolderPath, $"FixedAudio{counter}.flv");
            var firstCommand = FFMPEGManager.GetMergeAudioCommand(audioLineData.Result, dataFolderPath, firstAudioPath);
            ffmpegCommands.Add(firstCommand);
            var streamData = new StreamData();
            streamData.FileNames = firstAudioPath;
            streamData.StartFilesTime = 0;
            streamData.EndFilesTime = FindMaxEndTime(audioLineData.Result);


            if (audioLineData.NewInput.Count != 0)
            {
                streamData.FileNames = streamData.FileNames.Substring(0, streamData.FileNames.Length - 4);
                streamDatas.Add(streamData);
                while (true)
                {
                    counter++;
                    var audioLine = AudioLine(audioLineData.NewInput);
                    ffmpegCommands.Add(FFMPEGManager.GetMergeAudioCommand(audioLine.Result, dataFolderPath, Path.Combine(dataFolderPath, $"FixedAudio{counter}.flv")));
                    var streamData1 = new StreamData
                    {
                        FileNames = Path.Combine(dataFolderPath, $"FixedAudio{counter}"),
                        StartFilesTime = 0,
                        EndFilesTime = FindMaxEndTime(audioLine.Result)
                    };
                    streamDatas.Add(streamData1);

                    if (audioLine.NewInput.Count == 0)
                        break;
                    audioLineData = audioLine;
                }

                var finalAudio = FFMPEGManager.GetMergeAudioCommand(streamDatas, dataFolderPath, finalAudionPath);
                ffmpegCommands.Add(finalAudio);
            }
            else
            {
                ffmpegCommands[0] = ffmpegCommands[0].Replace(firstAudioPath, finalAudionPath);
            }


            var processStartInfo = new ProcessStartInfo(ffmpegAddress);
            FFMPEGManager.RunMultiProcess(processStartInfo, ffmpegCommands);

            return finalAudionPath;
        }

        public uint FindMinStartTime(List<StreamData> streamDatas)
        {
            uint res = streamDatas[0].StartFilesTime;

            foreach (var item in streamDatas)
            {
                if (item.StartFilesTime < res)
                    res = item.StartFilesTime;
            }

            return res;
        }

        public uint FindMaxEndTime(List<StreamData> streamDatas)
        {
            uint res = streamDatas[0].EndFilesTime;

            foreach (var item in streamDatas)
            {
                if (item.EndFilesTime > res)
                {
                    res = item.EndFilesTime;
                }
            }

            return res;
        }

        private AudioLineData AudioLine(List<StreamData> audioStreamDatas)
        {

            StreamData lastData = audioStreamDatas[0];
            var streams = new List<StreamData>();
            var copyData = new List<StreamData>(audioStreamDatas);

            streams.Add(lastData);
            copyData.Remove(lastData);

            for (int i = 1; i < audioStreamDatas.Count; i++)
            {
                if (lastData.EndFilesTime <= audioStreamDatas[i].StartFilesTime)
                {
                    lastData = audioStreamDatas[i];
                    streams.Add(lastData);
                    copyData.Remove(lastData);
                }
            }


            return new AudioLineData
            {
                NewInput = copyData,
                Result = streams
            };
        }
    }
}
