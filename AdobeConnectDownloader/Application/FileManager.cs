using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
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

    public class AudioManager
    {
        public FFMPEGManager FFMPEGManager { get; set; }

        public AudioManager(FFMPEGManager fFMPEGManager)
        {
            FFMPEGManager = fFMPEGManager;
        }

        public static void CreateWaveFile(int miliSecond, string pathFile)
        {
            var ChunID = Encoding.ASCII.GetBytes("RIFF"); //  Contains the letters "RIFF" in ASCII form (0x52494646 big-endian form)

            var AudioFormat = Encoding.ASCII.GetBytes("WAVE"); // Contains the letters "WAVE" in ASCII form (0x57415645 big-endian form)

            var SubChunk1ID = Encoding.ASCII.GetBytes("fmt "); // Contains the letters "fmt " (0x666d7420 big-endian form)

            var SubChunk1Size = 16; // 16 for PCM.  This is the size of the rest of the Subchunk which follows this number.

            var AudioFormat1 = 1; // PCM = 1 (i.e. Linear quantization) Values other than 1 indicate some form of compression.

            var NumChannels = 1; // Mono = 1, Stereo = 2, etc.

            var BitsPerSample = 8; // 8 bits = 8, 16 bits = 16, etc.

            var SampleRate = 44100; // 8000, 44100, etc.

            uint ByteRate = (uint)(SampleRate * NumChannels * (BitsPerSample / 8));

            var NumBytes = (uint)(miliSecond * ByteRate / 1000);

            ushort BlockAlign = (ushort)(NumChannels * (BitsPerSample / 8));

            var SubChunk2ID = Encoding.ASCII.GetBytes("data"); // Contains the letters "data" (0x64617461 big-endian form)

            uint SubChunk2Size = (uint)(NumBytes * NumChannels * (BitsPerSample / 8)); // This is the number of bytes in the data. You can also think of this as the size of the read of the subchunk following this number

            uint ChunkSize = (uint)(36 + SubChunk2Size); // or 4 + (8 + SubChunk1Size) + (8 + SubChunk2Size)

            byte[] data = new byte[NumBytes];


            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }

            using (FileStream fs = new FileStream(pathFile, FileMode.Create))
            {
                fs.Write(ChunID, 0, ChunID.Length);
                fs.Write(BitConverter.GetBytes(ChunkSize), 0, 4);
                fs.Write(AudioFormat, 0, AudioFormat.Length);
                fs.Write(SubChunk1ID, 0, SubChunk1ID.Length);
                fs.Write(BitConverter.GetBytes(SubChunk1Size), 0, 4);
                fs.Write(BitConverter.GetBytes(AudioFormat1), 0, 2);
                fs.Write(BitConverter.GetBytes(NumChannels), 0, 2);
                fs.Write(BitConverter.GetBytes(SampleRate), 0, 4);
                fs.Write(BitConverter.GetBytes(ByteRate), 0, 4);
                fs.Write(BitConverter.GetBytes(BlockAlign), 0, 2);
                fs.Write(BitConverter.GetBytes(BitsPerSample), 0, 2);
                fs.Write(SubChunk2ID, 0, SubChunk2ID.Length);
                fs.Write(BitConverter.GetBytes(SubChunk2Size), 0, 4);
                fs.Write(data, 0, data.Length);
            }
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

    public class XmlReader
    {
        public static ListOfStreamData GetTimesOfFiles(List<string> fileNames, string xmlFileData, uint endTimeOfRoom)
        {
            List<StreamData> AudioStreamData = new List<StreamData>();
            List<StreamData> ScreenStreamData = new List<StreamData>();

            foreach (var item in fileNames)
            {
                if (item.StartsWith("cameraVoip_"))
                {
                    string filename = item.Substring(0, item.Length - 4);
                    var x = GetStreamTimes(xmlFileData, filename, endTimeOfRoom);
                    AudioStreamData.Add(x);

                }
                else if (item.StartsWith("screenshare_"))
                {
                    string filename = item.Substring(0, item.Length - 4);
                    var x = GetStreamTimes(xmlFileData, filename, endTimeOfRoom);
                    ScreenStreamData.Add(x);
                }
            }

            AudioStreamData = AudioStreamData.OrderBy(i => i.StartFilesTime).ToList();

            if (ScreenStreamData.Count != 0 || ScreenStreamData.Count != 1)
                ScreenStreamData = ScreenStreamData.OrderBy(i => i.StartFilesTime).ToList();

            return new ListOfStreamData()
            {
                AudioStreamData = AudioStreamData,
                ScreenStreamData = ScreenStreamData
            };

        }

        private static StreamData GetStreamTimes(string xmlFileData, string searchData, uint endTimeOfRoom)
        {
            string streamNameXml = $"<streamName><![CDATA[/{searchData}]]></streamName>";
            string startTimeString = "<time><![CDATA[";
            string endStartTimestring = "]]></time>";
            uint EndFileTime = endTimeOfRoom;

            int index1 = xmlFileData.IndexOf(streamNameXml) + streamNameXml.Length;

            int startTimeIndex = xmlFileData.IndexOf(startTimeString, index1) + startTimeString.Length;
            int endTimeIndex = xmlFileData.IndexOf(endStartTimestring, startTimeIndex);
            string startTime = xmlFileData.Substring(startTimeIndex, (endTimeIndex - startTimeIndex));


            int index2 = xmlFileData.IndexOf(streamNameXml, index1);

            if (index2 != -1)
            {
                int endTimeValueIndex = xmlFileData.IndexOf(startTimeString, index2) + startTimeString.Length;
                int endTimeValueIndex2 = xmlFileData.IndexOf(endStartTimestring, endTimeValueIndex);
                string endTimeValue = xmlFileData.Substring(endTimeValueIndex, (endTimeValueIndex2 - endTimeValueIndex));
                EndFileTime = uint.Parse(endTimeValue);
            }

            return new StreamData()
            {
                FileNames = searchData,
                StartFilesTime = uint.Parse(startTime),
                EndFilesTime = EndFileTime
            };
        }

        public static uint GetEndOfTime(string xmlFileData)
        {
            string endTimeString = "<String><![CDATA[__stop__]]></String>";
            string numberStr = "<Number><![CDATA[";
            string endNumberStr = "]]></Number>";

            int indexStop = xmlFileData.IndexOf(endTimeString) + endTimeString.Length;
            int indexData1 = xmlFileData.IndexOf(numberStr, indexStop) + numberStr.Length;
            int indexData2 = xmlFileData.IndexOf(endNumberStr, indexData1);
            string timeValue = xmlFileData.Substring(indexData1, indexData2 - indexData1);

            uint data = uint.Parse(timeValue);

            return data;
        }

    }

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
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"CustomVideo{counter}.flv");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(firstTime, folderPathForCreateFiles, videoSize, imageAddress, fileAddress);

                FFMPEGManager.RunProcess(processStartInfo, command);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[0].FileNames + ".flv"));
                counter++;
            }

            for (int i = 1; i < streamDatas.Count; i++)
            {
                uint offset = streamDatas[i].StartFilesTime - streamDatas[i - 1].EndFilesTime;
                string fileAddress = Path.Combine(outputFolderForSyncVideo, $"CustomVideo{counter}.flv");
                string command = FFMPEGManager.GetCommandForCreateEmptyVideo(offset, folderPathForCreateFiles, videoSize, imageAddress, fileAddress);
                FFMPEGManager.RunProcess(processStartInfo, command);
                res.Add(fileAddress);
                res.Add(Path.Combine(extractedDataFolder, streamDatas[i].FileNames + ".flv"));
                counter++;
            }
            return res;
        }
    }

    public class AudioLineData
    {
        public List<StreamData> Result { get; set; } = new List<StreamData>();
        public List<StreamData> NewInput { get; set; } = new List<StreamData>();
    }
}
