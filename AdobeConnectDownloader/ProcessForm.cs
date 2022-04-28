using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using AdobeConnectDownloader.Application;

namespace AdobeConnectDownloader
{
    public partial class ProcessForm : Form
    {
        public string Url { get; set; } = string.Empty;

        public string WorkFolderPath = string.Empty;
        public string ExtractFolder { get; set; } = String.Empty;
        public string ZipFileAddress { get; set; } = String.Empty;
        public string CustomVideoForGetResolotion { get; set; } = String.Empty;
        public string FFMPEGAddress { get; set; } = String.Empty;
        public string NotAvailableVideoImageAddress { get; set; } = String.Empty;

        public bool CancelProcess { get; set; } = false;
        public bool IsEverythingOk { get; set; } = false;

        public FFMPEGManager FFMPEGManager = new FFMPEGManager();

        public FileManager FileManager = null;
        public WebManager WebManager = new WebManager();

        public AudioManager AudioManager = null;
        public VideoManager VideoManager = null;

        public DataGridView QueeDataGridView { get; set; } = null;

        public ProcessForm()
        {
            InitializeComponent();

            AudioManager = new AudioManager(FFMPEGManager);
            VideoManager = new VideoManager(FFMPEGManager);
        }

        private async void ProcessForm_Load(object sender, EventArgs e)
        {
            ExtractFolder = Path.Combine(WorkFolderPath, "Extracted Data");
            FileManager = new FileManager();

            if (Directory.Exists(Path.Combine(WorkFolderPath, "Extracted Data")) == false)
                Directory.CreateDirectory(Path.Combine(WorkFolderPath, "Extracted Data"));


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

                bool wrongUrl = WebManager.IsUrlWrong(downloadUrl.Url, WebManager.GetCookieForm(url, sessionCookie));
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
            await Task.Factory.StartNew(() =>
            {
                DownloadProcessLabel.ForeColor = Color.Green;

                var zipEntriesName = FileManager.ExtractZipFile(ZipFileAddress, ExtractFolder);

                if (ExtractFolder.Length == 0)
                {
                    MessageBox.Show("Your zip file have wrong format please try againg");
                    return;
                }

                ExtractZipDataLabel.ForeColor = Color.Green;

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled Wait");
                    return;
                }

                Model.ListOfStreamData filesTime = GetStreamData(zipEntriesName);

                GetStreamsDataLabel.ForeColor = Color.Green;

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled Please Wait");
                    return;
                }

                AudioManager.FFMPEGManager = FFMPEGManager;
                string finalAudioAddress = AudioManager.MatchAllAudio(filesTime.AudioStreamData, ExtractFolder, WorkFolderPath, FFMPEGAddress);

                FixAudiosLabel.ForeColor = Color.Green;

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled Please Wait");
                    return;

                }

                string finalVideoAddress = Path.Combine(WorkFolderPath, "Final Video Witout Sound.flv");
                if (filesTime.ScreenStreamData.Count != 0)
                {
                    GetFinalVideo(filesTime, finalVideoAddress, finalAudioAddress);
                }

                SyncAllDataLabel.ForeColor = Color.Green;

                Directory.Delete(ExtractFolder, true);
            });
        }

        private void GetFinalVideo(Model.ListOfStreamData filesTime, string finalVideoAddress, string finalAudioAddress)
        {
            CustomVideoForGetResolotion = Path.Combine(WorkFolderPath, filesTime.ScreenStreamData[0].FileNames + ".flv");

            string videoSize = FFMPEGManager.GetVideosResolotion(Path.Combine(ExtractFolder, filesTime.ScreenStreamData[0].FileNames + ".flv"), FFMPEGAddress);

            VideoManager.FFMPEGManager = FFMPEGManager;
            var y = VideoManager.GetVideoLine(filesTime.ScreenStreamData, ExtractFolder, ExtractFolder, videoSize, NotAvailableVideoImageAddress, ExtractFolder, FFMPEGAddress);

            string ffmpegCommand = FFMPEGManager.CreateConcatFile(y, finalVideoAddress);
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = FFMPEGAddress;
            FFMPEGManager.RunProcess(processStartInfo, ffmpegCommand);

            FixVideosLabel.ForeColor = Color.Green;


            string finalCommandFormMixAudioAndVideo = $"-hide_banner -i \"{finalVideoAddress}\" -i \"{finalAudioAddress}\" -c:v copy -c:a aac -y -shortest \"{Path.Combine(WorkFolderPath, "Final Room Video.flv")}\"";
            FFMPEGManager.RunProcess(processStartInfo, finalCommandFormMixAudioAndVideo);
        }

        private Model.ListOfStreamData GetStreamData(List<string> zipEntriesName)
        {
            string xmlFileData = File.ReadAllText(zipEntriesName.Find(i => i.EndsWith("indexstream.xml") == true));
            uint endRoomTime = XmlReader.GetEndOfTime(xmlFileData);
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
            }

        }


    }
}
