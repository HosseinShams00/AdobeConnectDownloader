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

namespace AdobeConnectDownloader.UI
{
    public partial class ProcessForm : Form
    {
        public string Url { get; set; } = null;

        public string WorkFolderPath = string.Empty;
        public string ExtractFolder { get; set; } = String.Empty;
        public string ZipFileAddress { get; set; } = String.Empty;
        public string CustomVideoForGetResolotion { get; set; } = String.Empty;
        public string FFMPEGAddress { get; set; } = String.Empty;
        public string SwfFileAddress { get; set; } = string.Empty;
        public string NotAvailableVideoImageAddress { get; set; } = String.Empty;
        public string Title { get; set; }
        public string swfFolder { get; set; } = String.Empty;

        public bool CancelProcess { get; set; } = false;
        public bool IsEverythingOk { get; set; } = true;

        public FFMPEGManager FFMPEGManager = new FFMPEGManager();

        public FileManager FileManager = null;
        public WebManager WebManager = new WebManager();
        public SwfManager SwfManager;

        public AudioManager AudioManager = null;
        public VideoManager VideoManager = null;

        public DataGridView QueueDataGridView { get; set; } = null;
        private List<Cookie> Cookies { get; set; } = new List<Cookie>();
        public uint endRoomTime { get; private set; }

        public ProcessForm()
        {
            InitializeComponent();

            AudioManager = new AudioManager(FFMPEGManager);
            VideoManager = new VideoManager(FFMPEGManager);
        }

        private async void ProcessForm_Load(object sender, EventArgs e)
        {
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
                DownloadProcessLabel.ForeColor = Color.Green;

                var zipEntriesName = FileManager.ExtractZipFile(ZipFileAddress, ExtractFolder);

                if (zipEntriesName.Count == 0)
                {
                    MessageBox.Show("Your zip file have wrong format please try againg");
                    return;
                }

                ExtractZipDataLabel.ForeColor = Color.Green;

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled");
                    return;
                }

                Model.ListOfStreamData filesTime = GetStreamData(zipEntriesName);

                if (filesTime == null)
                    GetStreamsDataLabel.ForeColor = Color.Red;
                else
                    GetStreamsDataLabel.ForeColor = Color.Green;

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled");
                    return;
                }

                if (Url != null)
                {
                    bool checkAssetsMethod = DownloadAssetsMethod1(Url, Cookies);

                    if (CancelProcess == true)
                    {
                        MessageBox.Show("Process Caneled");
                        return;
                    }

                    if (checkAssetsMethod == false)
                    {
                        var method2 = DownloadAssetsMethod2(zipEntriesName, Cookies, WorkFolderPath);

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

                string finalVideoAddress = Path.Combine(WorkFolderPath, "Final Meeting Video.flv");
                if (filesTime.ScreenStreamData.Count != 0)
                {
                    GetFinalVideo(filesTime, endRoomTime, finalVideoAddress, finalAudioAddress);
                }
                else
                    FixVideosLabel.ForeColor = Color.Red;

                SyncAllDataLabel.ForeColor = Color.Green;
            });
        }

        private bool DownloadAssetsMethod1(string url, List<Cookie> cookies)
        {
            string assetUrl = WebManager.GetAssetsDownloadUrl(url);
            string fileAddress = Path.Combine(WorkFolderPath, "Assets.zip");

            var downloadResult = WebManager.GetStreamData(assetUrl, cookies, WebManager.HttpContentType.Zip, fileAddress, true);

            return downloadResult;
        }

        private bool DownloadAssetsMethod2(List<string> zipEntriesName, List<Cookie> Cookies, string outputFolder)
        {
            string xmlFileData = File.ReadAllText(zipEntriesName.Find(i => i.EndsWith("indexstream.xml") == true));
            string baseUrl = Url.Substring(0, Url.IndexOf(".ir/") + 4);
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
            CustomVideoForGetResolotion = Path.Combine(WorkFolderPath, filesTime.ScreenStreamData[0].FileNames + ".flv");

            string videoSize = FFMPEGManager.GetVideosResolotion(Path.Combine(ExtractFolder, filesTime.ScreenStreamData[0].FileNames + ".flv"), FFMPEGAddress);

            VideoManager.FFMPEGManager = FFMPEGManager;
            var y = VideoManager.GetVideoLine(filesTime.ScreenStreamData, endTime, ExtractFolder, ExtractFolder, videoSize, NotAvailableVideoImageAddress, ExtractFolder, FFMPEGAddress);

            string ffmpegCommand = FFMPEGManager.CreateConcatFile(y, finalAudioAddress, finalVideoAddress);
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = FFMPEGAddress;
            FFMPEGManager.RunProcess(processStartInfo, ffmpegCommand);

            FixVideosLabel.ForeColor = Color.Green;
        }

        private ListOfStreamData GetStreamData(List<string> zipEntriesName)
        {

            string xmlAddress = zipEntriesName.Find(i => i.EndsWith("indexstream.xml") == true);
            if (xmlAddress == null)
                return null;

            string xmlFileData = File.ReadAllText(xmlAddress);
            endRoomTime = XmlReader.GetEndOfTime(xmlFileData);
            var filesTime = XmlReader.GetTimesOfFiles(FileManager.GetZipFilesName(ZipFileAddress), xmlFileData, endRoomTime);
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
