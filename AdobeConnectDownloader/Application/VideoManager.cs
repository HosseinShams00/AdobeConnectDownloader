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
            string extractedDataFolder, string videoSize, string imageAddress, string outputFolderForSyncVideo,
            string customNameForEmptyVideos = "EmptyVideo")
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
            string imageAddressCopy = Path.Combine(folderPathForCreateFiles, "No Available Video.png");

            if (File.Exists(imageAddressCopy))
                File.Delete(imageAddressCopy);

            image.Save(imageAddressCopy);
            image.Dispose();
            imageCopy.Dispose();

            if (firstTime != 0)
            {
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"{customNameForEmptyVideos}{counter}.flv");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(firstTime, imageAddressCopy, fileAddress);
                processStartInfo.Arguments = command;
                FFMPEGManager.RunProcess(processStartInfo);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[0].FileNames + ".flv"));
                counter++;
            }

            for (int i = 1; i < streamDatas.Count; i++)
            {
                uint offset = streamDatas[i].StartFilesTime - streamDatas[i - 1].EndFilesTime;
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"{customNameForEmptyVideos}{counter}.flv");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(offset, imageAddressCopy, fileAddress);
                processStartInfo.Arguments = command;
                FFMPEGManager.RunProcess(processStartInfo);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[i].FileNames + ".flv"));
                counter++;
            }

            uint Finaloffset = EndTime - streamDatas[streamDatas.Count - 1].EndFilesTime;
            string FinalfileAddress = Path.Combine(outputFolderForSyncVideo, $"{customNameForEmptyVideos}{counter}.flv");
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
                string fileAddress = Path.Combine(extractedFolder, audioData.FileNames + ".flv");
                string ffmpegCommand = $"-hide_banner -i \"{fileAddress}\"";
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = ffmpegAddress;
                processStartInfo.Arguments = ffmpegCommand;
                string data = FFMPEGManager.RunProcessAndGetOutput(processStartInfo);

                var index1 = data.IndexOf("Duration:");
                int videoIndex = data.IndexOf("Video", index1);
                if (videoIndex != -1)
                {
                    string newFileAddress = Path.Combine(extractedFolder, audioData.FileNames + "_WitoutSound.flv");
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
