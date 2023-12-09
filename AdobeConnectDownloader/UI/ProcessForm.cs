using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using AdobeConnectDownloader.Application;
using System.Net;
using AdobeConnectDownloader.Model;
using System.Threading;

namespace AdobeConnectDownloader.UI
{
    public partial class ProcessForm : Form
    {
        public string? Url { get; set; } = null;

        public string WorkFolderPath { get; set; } = string.Empty;
        public string ExtractFolder { get; set; } = string.Empty;
        public string ZipFileAddress { get; set; } = string.Empty;
        public string CustomVideoForGetResolotion { get; set; } = string.Empty;
        public string FFMPEGAddress { get; set; } = string.Empty;
        public string SwfFileAddress { get; set; } = string.Empty;
        public string NotAvailableVideoImageAddress { get; set; } = string.Empty;
        public string Title { get; set; }
        public string swfFolder { get; set; } = string.Empty;

        public bool CancelProcess { get; set; } = false;
        public bool IsEverythingOk { get; set; } = true;
        public bool JustDownloadFiles { get; set; }

        public FFMPEGManager FFMPEGManager = new FFMPEGManager();

        public FileManager? FileManager = null;
        public WebManager WebManager = new WebManager();
        public SwfManager SwfManager;

        public AudioManager? AudioManager = null;
        public VideoManager? VideoManager = null;

        public DataGridView? QueueDataGridView { get; set; } = null;
        private List<Cookie> Cookies { get; set; } = new List<Cookie>();
        public uint EndRoomTime { get; private set; }

        public ProcessForm()
        {
            InitializeComponent();
        }

        private async void ProcessForm_Load(object sender, EventArgs e)
        {
            AudioManager = new AudioManager(FFMPEGManager);
            VideoManager = new VideoManager(FFMPEGManager, FFMPEGAddress);
            SwfManager = new SwfManager(SwfFileAddress);


            this.Text += " " + Title;
            ExtractFolder = Path.Combine(WorkFolderPath, "Extracted Data");
            FileManager = new FileManager();

            if (Directory.Exists(Path.Combine(WorkFolderPath, "Extracted Data")) == false)
                Directory.CreateDirectory(Path.Combine(WorkFolderPath, "Extracted Data"));


            if (File.Exists(swfFolder) == false)
                Directory.CreateDirectory(swfFolder);

            if (ZipFileAddress == string.Empty)
            {
                await DownloadZipFile(Url);
            }
            else
            {
                await MergeAllFiles();
                this.Close();
            }
        }

        public async Task DownloadZipFile(string url)
        {
            var downloadUrl = WebManager.GetDownloadUrl(url);
            WebManager.PercentageChange += WebManager_PercentageChange;
            WebManager.DownloadFileComplited += WebManager_DownloadFileComplited;
            try
            {
                ZipFileAddress = Path.Combine(WorkFolderPath, downloadUrl.FileId + ".zip");
                var sessionCookie = WebManager.GetSessionCookieFrom(url);
                Cookies = WebManager.GetCookieForm(url, sessionCookie);

                bool wrongUrl = WebManager.IsUrlWrong(downloadUrl.Url, Cookies);
                if (wrongUrl == true)
                {
                    MessageBox.Show("Your url is worng maybe your not login please login and try again");
                    IsEverythingOk = false;
                    return;
                }

                await WebManager.DownloadFile(downloadUrl.Url, ZipFileAddress, sessionCookie);
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                IsEverythingOk = true;
                return;
            }
        }

        public void WebManager_PercentageChange(int percent, double byteRecive, double TotalByte)
        {
            if (CancelProcess == false)
            {
                string downloadRes = $"{byteRecive.ToString("F2")} MB / {TotalByte.ToString("F2")} MB";
                if (DownloadProcessLabel.InvokeRequired)
                {
                    DownloadProcessLabel.Invoke(new Action<int, double, double>(WebManager_PercentageChange), downloadRes);
                    DownloadProgressBar.Value = percent;

                    return;
                }

                DownloadProcessLabel.Text = downloadRes;
                DownloadProgressBar.Value = percent;
            }
            else
            {
                WebManager.CancelDownload();
                this.Close();
            }

        }

        private async void WebManager_DownloadFileComplited(object? sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null && e.Cancelled == false)
            {
                await MergeAllFiles();
            }
            else
            {
                MessageBox.Show("Download Process Canceled");

            }

            this.Close();
        }

        private async Task MergeAllFiles()
        {
            await Task.Run(() =>
            {
                DownloadProgressBar.Invoke(new Action(() =>
                {
                    DownloadProgressBar.Value = 100;
                    DownloadProcessLabel.Text = "100%";
                    DownloadProcessLabel.ForeColor = Color.Green;
                }));

                var zipEntriesName = FileManager.ExtractZipFile(ZipFileAddress, ExtractFolder);

                if (zipEntriesName.Count == 0)
                {
                    MessageBox.Show("Your zip file have wrong format please try againg");
                    return;
                }


                ExtractZipDataLabel.ForeColor = Color.Green;
                //string xmlFileData = File.ReadAllText(zipEntriesName.Find(i => i.EndsWith("indexstream.xml") == true));
                string xmlFileData = File.ReadAllText(zipEntriesName.Find(i => i.EndsWith("mainstream.xml") == true));

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled");
                    return;
                }

                ListOfStreamData filesTime = GetStreamData(xmlFileData);

                GetStreamsDataLabel.ForeColor = Color.Green;

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled");
                    return;
                }

                if (Url != null)
                {
                    var baseUrl = Url.Substring(0, Url.IndexOf("/", 9) + 1);

                    WebManager.GetFiles(baseUrl, xmlFileData, Cookies, WorkFolderPath);

                    if (CancelProcess == true)
                    {
                        MessageBox.Show("Process Caneled");
                        return;
                    }

                    var checkAssetsMethod = WebManager.DownloadAssetsMethod1(Url, Cookies, WorkFolderPath);

                    if (CancelProcess == true)
                    {
                        MessageBox.Show("Process Caneled");
                        return;
                    }

                    if (checkAssetsMethod == false)
                    {
                        var method2 = DownloadAssetsMethod2(baseUrl, xmlFileData, Cookies, WorkFolderPath);

                        if (method2 == true)
                            DownloadAssetsLabel.ForeColor = Color.Green;
                        else
                            DownloadAssetsLabel.ForeColor = Color.Red;
                    }
                    else
                        DownloadAssetsLabel.ForeColor = Color.Green;

                    if (CancelProcess == true)
                    {
                        MessageBox.Show("Process Caneled");
                        return;
                    }
                }
                else
                    DownloadAssetsLabel.ForeColor = Color.Red;

                if (JustDownloadFiles)
                {
                    IsEverythingOk = true;
                    return;
                }

                if (filesTime == null)
                {
                    MessageBox.Show("Have a problem");
                    return;
                }

                AudioManager.FFMPEGManager = FFMPEGManager;
                string finalAudioAddress = AudioManager.MatchAllAudio(filesTime.AudioStreamData, ExtractFolder, WorkFolderPath, FFMPEGAddress);

                FixAudiosLabel.ForeColor = Color.Green;

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled");
                    return;
                }

                string finalVideoAddress = null;
                if (filesTime.ScreenStreamData.Count != 0)
                {
                    finalVideoAddress = Path.Combine(WorkFolderPath, "Final Meeting Video.flv");
                    FixVideoTime(filesTime, ExtractFolder);
                    GetFinalVideo(filesTime, EndRoomTime, finalVideoAddress, finalAudioAddress);
                }
                else
                    FixVideosLabel.ForeColor = Color.Red;

                var result = VideoManager.ChekHaveWebcamVideo(filesTime.AudioStreamData, ExtractFolder);

                if (result.Count == 0)
                    CheckWebcamLabel.ForeColor = Color.Red;
                else
                    CheckWebcamLabel.ForeColor = Color.Green;

                bool isWebcamFixed = CheckWebcam(result, finalAudioAddress, finalVideoAddress);


                SyncAllDataLabel.ForeColor = Color.Green;

            });
        }

        private void FixVideoTime(ListOfStreamData filesTime, string extractFolder)
        {
            int counter = 0;
            foreach (var videoStream in filesTime.ScreenStreamData)
            {
                var fileAddress = Path.Combine(extractFolder, videoStream.FileNames + videoStream.Extension);
                var outputFileAddress = Path.Combine(extractFolder, videoStream.FileNames + $"_{counter}_" + videoStream.Extension);

                Application.FFMPEGManager.FixVideoTime(fileAddress, outputFileAddress, FFMPEGAddress);
                var time = FFMPEGManager.GetVideoTime(outputFileAddress, FFMPEGAddress);

                videoStream.StartFilesTime += (videoStream.EndFilesTime - videoStream.StartFilesTime) - Helper.Time.ConvertTimeToMilisecond(time);
                videoStream.FileNames += $"_{counter}_";
                counter++;
            }
        }

        private bool CheckWebcam(List<StreamData> webcamStreams, string finalAudioAddress, string finalVideoAddress)
        {
            if (webcamStreams.Count != 0)
            {
                CustomVideoForGetResolotion = Path.Combine(ExtractFolder, webcamStreams[0].FileNames + ".flv");

                string videoSize = FFMPEGManager.GetVideosResolution(Path.Combine(ExtractFolder, webcamStreams[0].FileNames + ".flv"), FFMPEGAddress);

                VideoManager.FFMPEGManager = FFMPEGManager;
                var videoLines = VideoManager.GetVideoLine(webcamStreams, EndRoomTime, ExtractFolder, ExtractFolder, videoSize, NotAvailableVideoImageAddress, ExtractFolder, "WebCamVideo");
                string webcamVideoFileAddress = Path.Combine(WorkFolderPath, "Final WebCam Video Witout Sound.flv");
                string ffmpegCommand = FFMPEGManager.CreateConcatFile(videoLines, webcamVideoFileAddress);
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = FFMPEGAddress;
                processStartInfo.Arguments = ffmpegCommand;
                FFMPEGManager.RunProcess(processStartInfo);
                string finalVideoAddressForWebcam = Path.Combine(WorkFolderPath, "Final Video With Webcam.flv");
                string command = string.Empty;

                if (finalVideoAddress == null)
                {
                    command = $"-hide_banner -i \"{finalAudioAddress}\" -i \"{webcamVideoFileAddress}\" -map 0:a -map 1:v -c:v copy -c:a copy -shortest -y \"{finalVideoAddressForWebcam}\"";
                }
                else
                {
                    string screenShareRes = FFMPEGManager.GetVideosResolution(finalVideoAddress, FFMPEGAddress);
                    string webcamRes = FFMPEGManager.GetVideosResolution(webcamVideoFileAddress, FFMPEGAddress);
                    int fullWidthRes = int.Parse(screenShareRes.Split('x')[0]) - int.Parse(webcamRes.Split('x')[0]);

                    command = $"-hide_banner -i {finalVideoAddress}  -i \"{webcamVideoFileAddress}\" " +
                    $"-filter_complex \"color=s={fullWidthRes + "x" + int.Parse(screenShareRes.Split('x')[1])}:c=black[base]; [0:v] setpts=PTS-STARTPTS[upperleft];[1:v]setpts=PTS-STARTPTS[upperright]; [base][upperleft]overlay=shortest=1[tmp1]; [tmp1][upperright] overlay=shortest=1:x={screenShareRes.Split('x')[0]}\"  -map 0:a -c:a copy -y -shortest \"{finalVideoAddressForWebcam}\"";
                }

                processStartInfo.Arguments = command;
                FFMPEGManager.RunProcess(processStartInfo);
                return true;
            }
            else
                return false;
        }

        public bool DownloadAssetsMethod2(string baseUrl, string xmlFileData, List<Cookie> Cookies, string outputFolder)
        {
            var baseDownloadAssetUrls = XmlReader.GetDefaultPdfPathForDownload(xmlFileData, baseUrl);

            if (baseDownloadAssetUrls.Count != 0 && Cookies.Count != 0)
            {
                int counter = 1;
                foreach (var baseDownloadAddress in baseDownloadAssetUrls)
                {
                    string response = GetDataForPdf(baseDownloadAddress);

                    if (response == null)
                        continue;

                    var pdfDetail = XmlReader.GetPdfDetail(response);
                    pdfDetail.FileName = Path.Combine(outputFolder, $"Pdf {counter}.pdf");

                    if (CancelProcess == true)
                    {
                        MessageBox.Show("Process Caneled");
                        return false;
                    }

                    DownloadSlides(pdfDetail, baseDownloadAddress, Cookies);

                    if (CancelProcess == true)
                    {
                        MessageBox.Show("Process Caneled");
                        return false;
                    }

                    SwfManager.ConvertSwfToPdf(pdfDetail, swfFolder);
                    counter++;
                    Directory.Delete(swfFolder, true);
                    Directory.CreateDirectory(swfFolder);
                }
                return true;
            }
            else
                return false;
        }

        private string GetDataForPdf(string defaultAddress)
        {

            string xmlPdfFilesname = defaultAddress + "layout.xml";
            Stream layoutStreamData = WebManager.GetStreamData(xmlPdfFilesname, Cookies, WebManager.HttpContentType.Xml);
            string response = string.Empty;

            if (layoutStreamData == null)
                return null;

            using (var reader = new StreamReader(layoutStreamData))
            {
                response = reader.ReadToEnd();
            }

            layoutStreamData.Dispose();
            return response;

        }

        private void DownloadSlides(PdfDetail pdfDetail, string baseUrlAddressForDownload, List<Cookie> Cookies)
        {
            for (int i = 1; i <= pdfDetail.PageNumber; i++)
            {
                string fileUrl = baseUrlAddressForDownload + i + ".swf";

                string counter = "";
                for (int j = 0; j < pdfDetail.PageNumber.ToString().Length - i.ToString().Length; j++)
                    counter += "0";

                string filePath = Path.Combine(swfFolder, counter + i + ".swf");

                try
                {
                    bool checkProblemWithFile = WebManager.GetStreamData(fileUrl, Cookies, WebManager.HttpContentType.Flash, filePath, true);
                    if (checkProblemWithFile == false)
                    {
                        MessageBox.Show("We Have Problem Try Again");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void GetFinalVideo(ListOfStreamData filesTime, uint endTime, string finalVideoAddress, string finalAudioAddress)
        {
            //CustomVideoForGetResolotion = Path.Combine(WorkFolderPath, filesTime.ScreenStreamData[0].FileNames + ".flv");

            string videoSize = FFMPEGManager.GetVideosResolution(Path.Combine(ExtractFolder, filesTime.ScreenStreamData[0].FileNames + filesTime.ScreenStreamData[0].Extension), FFMPEGAddress);

            VideoManager.FFMPEGManager = FFMPEGManager;
            var videoLine = VideoManager.GetVideoLine(filesTime.ScreenStreamData, endTime, ExtractFolder, ExtractFolder, videoSize, NotAvailableVideoImageAddress, ExtractFolder);
            //var videoLine = VideoManager.GetVideoLineV2(filesTime.ScreenStreamData, endTime, ExtractFolder, videoSize, NotAvailableVideoImageAddress, ExtractFolder);

            var ffmpegCommand = FFMPEGManager.CreateConcatFile(videoLine, finalAudioAddress, finalVideoAddress);
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = FFMPEGAddress;
            processStartInfo.Arguments = ffmpegCommand;
            FFMPEGManager.RunProcess(processStartInfo);

            FixVideosLabel.ForeColor = Color.Green;
        }

        private ListOfStreamData GetStreamData(string xmlFileData)
        {
            if (xmlFileData == null)
                return null;

            EndRoomTime = XmlReader.FindEndOfTimeV2(xmlFileData);
            //var filesTime = XmlReader.FindTimesOfFiles(FileManager.GetZipFilesName(ZipFileAddress), xmlFileData, EndRoomTime); // Old code
            var filesTime = XmlReader.FindTimesOfFilesV2(xmlFileData);
            FileManager.CheckHealthyFiles(filesTime, ExtractFolder, FFMPEGAddress);
            return filesTime;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ? ", "Cancel", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CancelProcess = true;
                IsEverythingOk = false;
                FFMPEGManager.CurrentProcess?.Kill();
                SwfManager.CancelProcess = true;
                SwfManager.CurrentProcess?.Kill();
            }

        }

        private void ProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists(ExtractFolder))
                Directory.Delete(ExtractFolder, true);


            if (Directory.Exists(swfFolder))
                Directory.Delete(swfFolder, true);
        }

    }
}
