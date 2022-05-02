using System.Collections.Generic;
using System.Diagnostics;
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

        public List<string> GetVideoLine(List<StreamData> streamDatas, string folderPathForCreateFiles, string extractedDataFolder, string videoSize, string imageAddress, string outputFolderForSyncVideo, string ffmpegAddress)
        {
            int counter = 0;
            List<string> res = new List<string>();
            uint firstTime = streamDatas[0].StartFilesTime;
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = ffmpegAddress;

            if (firstTime != 0)
            {
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"EmptyVideo{counter}.flv");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(firstTime, folderPathForCreateFiles, videoSize, imageAddress, fileAddress);

                FFMPEGManager.RunProcess(processStartInfo, command);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[0].FileNames + ".flv"));
                counter++;
            }

            for (int i = 1; i < streamDatas.Count; i++)
            {
                uint offset = streamDatas[i].StartFilesTime - streamDatas[i - 1].EndFilesTime;
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"EmptyVideo{counter}.flv");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(offset, folderPathForCreateFiles, videoSize, imageAddress, fileAddress);
                FFMPEGManager.RunProcess(processStartInfo, command);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[i].FileNames + ".flv"));
                counter++;
            }
            return res;
        }
    }
}
