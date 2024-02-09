using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using AdobeConnectDownloader.Model;


namespace AdobeConnectDownloader.Application
{
    public class VideoManager
    {
        public FFMPEGManager FFMPEGManager { get; set; }
        public string ffmpegAddress { get; set; }

        public VideoManager(FFMPEGManager fFMPEGManager, string ffmpegAddress)
        {
            FFMPEGManager = fFMPEGManager;
            this.ffmpegAddress = ffmpegAddress;
        }

        public List<string> GetVideoLine(List<StreamData> streamDatas, uint EndTime, string folderPathForCreateFiles,
            string extractedDataFolder, string imageAddress, string outputFolderForSyncVideo,
            string customNameForEmptyVideos = "EmptyVideo")
        {
            int counter = 0;
            List<string> res = new List<string>();
            uint firstTime = streamDatas[0].StartFilesTime;
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = ffmpegAddress;

            Image imageCopy = Image.FromFile(imageAddress);
            Bitmap image = new Bitmap(imageCopy, new Size(streamDatas[0].Width, streamDatas[0].Height));
            string imageAddressCopy = Path.Combine(folderPathForCreateFiles, "No Available Video.png");

            if (File.Exists(imageAddressCopy))
                File.Delete(imageAddressCopy);

            image.Save(imageAddressCopy);
            image.Dispose();
            imageCopy.Dispose();

            if (firstTime != 0)
            {
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"{customNameForEmptyVideos}{counter}{streamDatas[0].Extension}");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(firstTime, imageAddressCopy, fileAddress);
                processStartInfo.Arguments = command;
                FFMPEGManager.RunProcess(processStartInfo);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[0].FileNames + streamDatas[0].Extension));
                counter++;
            }

            for (int i = 1; i < streamDatas.Count; i++)
            {
                uint offset = streamDatas[i].StartFilesTime - streamDatas[i - 1].EndFilesTime;
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"{customNameForEmptyVideos}{counter}{streamDatas[i].Extension}");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(offset, imageAddressCopy, fileAddress);
                processStartInfo.Arguments = command;
                FFMPEGManager.RunProcess(processStartInfo);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[i].FileNames + streamDatas[i].Extension));
                counter++;
            }

            if (streamDatas[^1].EndFilesTime < EndTime)
            {
                uint finalOffset = EndTime - streamDatas[^1].EndFilesTime;
                var finalFileAddress = Path.Combine(outputFolderForSyncVideo, $"{customNameForEmptyVideos}{counter}{streamDatas[^1].Extension}");
                var finalCommand = FFMPEGManager.GetCommandForCreateEmptyVideo(finalOffset, imageAddressCopy, finalFileAddress);
                processStartInfo.Arguments = finalCommand;
                FFMPEGManager.RunProcess(processStartInfo);
                res.Add(finalFileAddress);
            }


            return res;
        }

        public List<string> GetVideoLineV2(List<StreamData> streamDatas, uint EndTime, string extractedDataFolder, string videoSize, string imageAddress, string outputFolderForSyncVideo)
        {
            int counter = 0;
            List<string> res = new List<string>();
            uint firstTime = streamDatas[0].StartFilesTime;
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = ffmpegAddress;

            int width = int.Parse(videoSize.Split('x')[0]);
            int height = int.Parse(videoSize.Split('x')[1]);
            Image imageCopy = Image.FromFile(imageAddress);
            Bitmap image = new Bitmap(imageCopy, new Size(width, height));
            string imageAddressCopy = Path.Combine(extractedDataFolder, "No Available Video.png");

            if (File.Exists(imageAddressCopy))
                File.Delete(imageAddressCopy);

            image.Save(imageAddressCopy);
            image.Dispose();
            imageCopy.Dispose();

            if (firstTime != 0)
            {
                var outputFileAddress = Path.Combine(outputFolderForSyncVideo, $"FixedVideo{counter}{streamDatas[0].Extension}");
                var videoAddress = Path.Combine(extractedDataFolder, streamDatas[0].FileNames + streamDatas[0].Extension);
                var command = FFMPEGManager.GetCommandForCreateEmptyVideoV2(videoAddress, firstTime, outputFileAddress);
                processStartInfo.Arguments = command;
                FFMPEGManager.RunProcess(processStartInfo);
                res.Add(outputFileAddress);
                //res.Add(Path.Combine(extractedDataFolder, streamDatas[0].FileNames + ".flv"));
                counter++;
            }

            for (int i = 1; i < streamDatas.Count; i++)
            {
                uint offset = streamDatas[i].StartFilesTime - streamDatas[i - 1].EndFilesTime;
                var outputFileAddress = Path.Combine(outputFolderForSyncVideo, $"FixedVideo{counter}{streamDatas[i].Extension}");
                var videoAddress = Path.Combine(extractedDataFolder, streamDatas[i].FileNames + streamDatas[i].Extension);
                var command = FFMPEGManager.GetCommandForCreateEmptyVideoV2(videoAddress, offset, outputFileAddress);
                processStartInfo.Arguments = command;
                FFMPEGManager.RunProcess(processStartInfo);
                res.Add(outputFileAddress);
                //res.Add(Path.Combine(extractedDataFolder, streamDatas[i].FileNames + ".flv"));
                counter++;
            }

            uint Finaloffset = EndTime - streamDatas[^1].EndFilesTime;
            string FinalfileAddress = Path.Combine(outputFolderForSyncVideo, $"FixedVideo{counter}{streamDatas[^1].Extension}");
            string Finalcommand = FFMPEGManager.GetCommandForCreateEmptyVideo(Finaloffset, imageAddressCopy, FinalfileAddress);
            processStartInfo.Arguments = Finalcommand;
            FFMPEGManager.RunProcess(processStartInfo);
            res.Add(FinalfileAddress);

            return res;
        }

        public List<StreamData> ChekHaveWebcamVideo(List<StreamData> audioStreamDatas, string extractedFolder)
        {
            List<StreamData> streamDatas = new List<StreamData>();

            foreach (var audioData in audioStreamDatas)
            {
                string fileAddress = Path.Combine(extractedFolder, audioData.FileNames + audioData.Extension);
                string ffmpegCommand = $"-hide_banner -i \"{fileAddress}\"";
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = ffmpegAddress;
                processStartInfo.Arguments = ffmpegCommand;
                string data = FFMPEGManager.RunProcessAndGetOutput(processStartInfo);

                var index1 = data.IndexOf("Duration:");
                int videoIndex = data.IndexOf("Video", index1);
                if (videoIndex != -1)
                {
                    string newFileAddress = Path.Combine(extractedFolder, audioData.FileNames + $"_WitoutSound{audioData.Extension}");
                    string originalWebcamVideoCommand = FFMPEGManager.RemoveAudioFromVideoCommand(fileAddress, newFileAddress);
                    processStartInfo.Arguments = originalWebcamVideoCommand;
                    FFMPEGManager.RunProcess(processStartInfo);
                    ffmpegCommand = $"-hide_banner -i \"{newFileAddress}\"";
                    processStartInfo.Arguments = ffmpegCommand;
                    data = FFMPEGManager.RunProcessAndGetOutput(processStartInfo);
                    index1 = data.IndexOf("Duration:") + "Duration:".Length;
                    int index2 = data.IndexOf(',', index1);
                    string duration = data.Substring(index1, index2 - index1).Trim();
                    uint miliSecond = Helper.Time.ConvertTimeToMilisecond(duration);
                    StreamData newStreamData = new StreamData();
                    newStreamData.FileNames = audioData.FileNames + "_WitoutSound";
                    newStreamData.StartFilesTime = audioData.StartFilesTime;
                    newStreamData.EndFilesTime = audioData.StartFilesTime + miliSecond;

                    streamDatas.Add(newStreamData);
                }
            }

            return streamDatas;
        }
    }
}
