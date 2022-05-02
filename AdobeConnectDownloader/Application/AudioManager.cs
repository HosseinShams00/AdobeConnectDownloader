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
            string finalAudionPath = string.Empty;
            var copy = audioStreamDatas;
            var x = AudioLine(copy);
            List<string> ffmpegCommands = new List<string>();

            int counter = 0;
            List<StreamData> streamDatas = new List<StreamData>();

            string firstAudioPath = Path.Combine(dataFolderPath, $"FixedAudio{counter}.flv");
            string firstCommand = FFMPEGManager.GetMergeAudioCommand(x.Result, dataFolderPath, firstAudioPath);
            ffmpegCommands.Add(firstCommand);
            StreamData streamData = new StreamData();
            streamData.FileNames = firstAudioPath;
            streamData.StartFilesTime = 0;
            streamData.EndFilesTime = GetMaxFromStartTime(x.Result);
            finalAudionPath = Path.Combine(outputFileFolder, "FinalAudio.flv");


            if (x.NewInput.Count != 0)
            {
                streamData.FileNames = streamData.FileNames.Substring(0, streamData.FileNames.Length - 4);
                streamDatas.Add(streamData);
                while (true)
                {
                    counter++;
                    var y = AudioLine(x.NewInput);
                    ffmpegCommands.Add(FFMPEGManager.GetMergeAudioCommand(y.Result, dataFolderPath, Path.Combine(dataFolderPath, $"FixedAudio{counter}.flv")));
                    StreamData streamData1 = new StreamData();
                    streamData1.FileNames = Path.Combine(dataFolderPath, $"FixedAudio{counter}");
                    streamData1.StartFilesTime = 0;
                    streamData1.EndFilesTime = GetMaxFromStartTime(y.Result);
                    streamDatas.Add(streamData1);

                    if (y.NewInput.Count == 0)
                        break;
                    x = y;
                }

                string finalAudio = FFMPEGManager.GetMergeAudioCommand(streamDatas, dataFolderPath, finalAudionPath);
                ffmpegCommands.Add(finalAudio);
            }
            else
            {
                ffmpegCommands[0] = ffmpegCommands[0].Replace(firstAudioPath, finalAudionPath);
            }


            ProcessStartInfo processStartInfo = new ProcessStartInfo(ffmpegAddress);
            FFMPEGManager.RunMultiProcess(processStartInfo, ffmpegCommands);

            return finalAudionPath;
        }

        public uint GetMinFromStartTime(List<StreamData> streamDatas)
        {
            uint res = streamDatas[0].StartFilesTime;

            foreach (var item in streamDatas)
            {
                if (item.StartFilesTime < res)
                    res = item.StartFilesTime;
            }

            return res;
        }

        public uint GetMaxFromStartTime(List<StreamData> streamDatas)
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
            List<StreamData> streams = new List<StreamData>();
            List<StreamData> copyData = new List<StreamData>(audioStreamDatas);

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
