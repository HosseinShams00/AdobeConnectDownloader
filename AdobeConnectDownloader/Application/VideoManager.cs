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

        public VideoManager(FFMPEGManager fFMPEGManager)
        {
            FFMPEGManager = fFMPEGManager;
        }

        public List<string> GetVideoLine(List<StreamData> streamDatas, uint EndTime, string folderPathForCreateFiles, string extractedDataFolder, string videoSize, string imageAddress, string outputFolderForSyncVideo, string ffmpegAddress)
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
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"EmptyVideo{counter}.flv");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(firstTime, imageAddressCopy, fileAddress);

                FFMPEGManager.RunProcess(processStartInfo, command);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[0].FileNames + ".flv"));
                counter++;
            }

            for (int i = 1; i < streamDatas.Count; i++)
            {
                uint offset = streamDatas[i].StartFilesTime - streamDatas[i - 1].EndFilesTime;
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"EmptyVideo{counter}.flv");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(offset, imageAddressCopy, fileAddress);
                FFMPEGManager.RunProcess(processStartInfo, command);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[i].FileNames + ".flv"));
                counter++;
            }

            uint Finaloffset = EndTime - streamDatas[streamDatas.Count - 1].EndFilesTime;
            string FinalfileAddress = Path.Combine(outputFolderForSyncVideo, $"EmptyVideo{counter}.flv");
            string Finalcommand = FFMPEGManager.GetCommandForCreateEmptyVideo(Finaloffset, imageAddressCopy, FinalfileAddress);
            FFMPEGManager.RunProcess(processStartInfo, Finalcommand);
            res.Add(FinalfileAddress);

            return res;
        }
    }
}
