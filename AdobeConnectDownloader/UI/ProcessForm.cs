using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AdobeConnectDownloader.Application;
using System.Net;
using System.Text;
using AdobeConnectDownloader.Model;
using System.Threading;
using AdobeConnectDownloader.Enums;

namespace AdobeConnectDownloader.UI;

public partial class ProcessForm : Form
{
    public string? Url { get; set; } = null;

    private string _fileName;
    public string FileName
    {
        get => _fileName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _fileName = "AdobeConnectMeetingFile";
            }
            else
            {
                _fileName = value;
            }
        }
    }

    public string WorkFolderPath { get; set; } = string.Empty;
    public string ExtractFolder { get; set; } = string.Empty;
    public string? ZipFileAddress { get; set; } = null;
    public string CustomVideoForGetResolution { get; set; } = string.Empty;
    public string FFMPEGAddress { get; set; } = string.Empty;
    public string SwfFileAddress { get; set; } = string.Empty;
    public string NotAvailableVideoImageAddress { get; set; } = string.Empty;
    public string Title { get; set; }
    public string SwfFolder { get; set; } = string.Empty;

    public bool CancelProcess { get; set; } = false;
    public bool IsEverythingOk { get; set; } = true;
    public bool JustDownloadFiles { get; set; }

    public FFMPEGManager FFMPEGManager = new FFMPEGManager();

    public FileManager FileManager = new();
    public WebManager WebManager = new();
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


        if (File.Exists(SwfFolder) == false)
            Directory.CreateDirectory(SwfFolder);

        if (string.IsNullOrWhiteSpace(ZipFileAddress))
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
        WebManager.DownloadFileComplited += WebManager_DownloadFileCompleted;
        try
        {
            ZipFileAddress = Path.Combine(WorkFolderPath, downloadUrl.FileId + ".zip");
            var sessionCookie = WebManager.GetSessionCookieFrom(url);
            Cookies = WebManager.GetCookieForm(url, sessionCookie);

            var isUrlWrong = WebManager.IsUrlWrong(downloadUrl.Url, Cookies);
            if (isUrlWrong == true)
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

    public void WebManager_PercentageChange(int percent, double byteReceive, double totalByte)
    {
        if (CancelProcess == false)
        {
            string downloadRes = $"{byteReceive.ToString("F2")} MB / {totalByte.ToString("F2")} MB";
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

    private async void WebManager_DownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
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
        await Task.Run(async () =>
        {
            try
            {
                DownloadProgressBar.Invoke(new Action(() =>
                {
                    DownloadProgressBar.Value = 100;
                    DownloadProcessLabel.Text = "100%";
                    DownloadProcessLabel.ForeColor = Color.Green;
                }));

                ChangeStatusInForm("Extract data", false);


                var zipEntriesName = FileManager.ExtractZipFile(ZipFileAddress, ExtractFolder);

                if (zipEntriesName.Count == 0)
                {
                    MessageBox.Show("Your zip file have wrong format please try againg");
                    return;
                }

                ChangeStatusInForm("Analyze data", true);

                //string xmlFileData = File.ReadAllText(zipEntriesName.Find(i => i.EndsWith("indexstream.xml") == true));
                string xmlFileData = await File.ReadAllTextAsync(zipEntriesName.Find(i => i.EndsWith("mainstream.xml")));

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled");
                    return;
                }

                ListOfStreamData? filesTime = GetStreamData(xmlFileData);

                xmlFileData = string.Empty;

                if (filesTime is null)
                {
                    IsEverythingOk = false;
                    MessageBox.Show("Process have problem with mainstream.xml of meeting");
                    return;
                }

                await LogMeetingDetails(filesTime);

                ChangeStatusInForm("Download assets", true);

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
                        DownloadAssetsMethod2(baseUrl, xmlFileData, Cookies, WorkFolderPath);
                    }

                    if (CancelProcess == true)
                    {
                        MessageBox.Show("Process Caneled");
                        return;
                    }
                }

                ChangeStatusInForm("Make Audio", true);

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
                string finalAudioAddress = AudioManager.MatchAllAudio(filesTime.AudioStreamData, FileName, ExtractFolder, WorkFolderPath, FFMPEGAddress);

                ChangeStatusInForm("Check Videos", true);

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled");
                    return;
                }

                string finalVideoAddress = null;
                if (filesTime.ScreenStreamData.Count != 0)
                {
                    finalVideoAddress = Path.Combine(WorkFolderPath, $"{FileName}.Video.flv");
                    FixVideoBugs(filesTime, ExtractFolder);
                    ChangeStatusInForm("Make video", true);

                    GetFinalVideo(filesTime, EndRoomTime, finalVideoAddress, finalAudioAddress);
                }

                ChangeStatusInForm("Check webcam", true);
                bool meetingHaveWebcamVideo = filesTime.WebCamStreamData.Count == 0;

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled");
                    return;
                }

                if (meetingHaveWebcamVideo == false)
                {
                    var confirmWebcam = MessageBox.Show("your meeting have webcam video do you need to process this ?", null, MessageBoxButtons.YesNo);
                    if (confirmWebcam == DialogResult.Yes)
                    {
                        ChangeStatusInForm("Make video with webcam video", true);
                        CheckWebcam(filesTime, finalAudioAddress, finalVideoAddress);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                IsEverythingOk = false;
                return;
            }

        });
    }

    private void ChangeStatusInForm(string text, bool gotoNextStep)
    {
        StatusLabel.Invoke(() =>
        {
            StatusLabel.Text = text;
            if (gotoNextStep)
            {
                StatusProgressBar.PerformStep();
            }
        });
    }

    private async Task LogMeetingDetails(ListOfStreamData filesTime)
    {
        var logFileAddress = Path.Combine(WorkFolderPath, "Log.txt");
        if (File.Exists(logFileAddress))
        {
            File.Delete(logFileAddress);
        }

        StringBuilder sb = new();
        sb.AppendLine($"File Name : {FileName}");
        sb.AppendLine($"Meeting time : {Helper.Time.ConvertUintToDurationV2(EndRoomTime)}");
        sb.AppendLine($"Count of audio file : {filesTime.AudioStreamData.Count}");

        foreach (var streamData in filesTime.AudioStreamData)
        {
            sb.AppendLine($" + {streamData.FileNames} {Helper.Time.ConvertUintToDurationV2(streamData.Length)}");
        }

        sb.AppendLine("".PadLeft(10, '-'));
        sb.AppendLine($"Count of video file : {filesTime.ScreenStreamData.Count}");

        foreach (var streamData in filesTime.ScreenStreamData)
        {
            sb.AppendLine($" + {streamData.FileNames} {Helper.Time.ConvertUintToDurationV2(streamData.Length)}");
        }

        sb.AppendLine("".PadLeft(10, '-'));
        sb.AppendLine($"Count of webcam file : {filesTime.WebCamStreamData.Count}");

        foreach (var streamData in filesTime.WebCamStreamData)
        {
            sb.AppendLine($" + {streamData.FileNames} {Helper.Time.ConvertUintToDurationV2(streamData.Length)}");
        }

        sb.AppendLine("".PadLeft(10, '='));

        await File.AppendAllTextAsync(logFileAddress, sb.ToString(), Encoding.UTF8);
    }

    private void FixVideoBugs(ListOfStreamData filesTime, string extractFolder)
    {
        int counter = 0;
        int minWidth = filesTime.ScreenStreamData.Min(x => x.Width);
        int minHeight = filesTime.ScreenStreamData.Min(x => x.Height);

        foreach (var videoStream in filesTime.ScreenStreamData)
        {
            var outputFileAddress = Path.Combine(extractFolder, videoStream.FileNames + $"_{counter}_" + videoStream.Extension);

            Application.FFMPEGManager.FixVideoBug(videoStream, ExtractFolder, minWidth, minHeight, outputFileAddress, FFMPEGAddress);
            var time = FFMPEGManager.GetVideoTime(outputFileAddress, FFMPEGAddress);

            videoStream.StartFilesTime += (videoStream.EndFilesTime - videoStream.StartFilesTime) - Helper.Time.ConvertTimeToMilisecond(time);
            videoStream.FileNames += $"_{counter}_";
            counter++;
        }
    }

    private bool CheckWebcam(ListOfStreamData streamsData, string finalAudioAddress, string finalVideoAddress)
    {
        if (streamsData.WebCamStreamData.Count != 0)
        {
            var firstWebcamVideo = streamsData.WebCamStreamData[0];
            CustomVideoForGetResolution = Path.Combine(ExtractFolder, firstWebcamVideo.FileNames + ".flv");

            VideoManager.FFMPEGManager = FFMPEGManager;
            var videoLines = VideoManager.GetVideoLine(streamsData.WebCamStreamData, EndRoomTime, ExtractFolder, NotAvailableVideoImageAddress, ExtractFolder, "WebCamVideo");
            string webcamVideoFileAddress = Path.Combine(WorkFolderPath, FileName + ".webcam_WithoutAudio.flv");
            string ffmpegCommand = FFMPEGManager.CreateConcatFile(videoLines, webcamVideoFileAddress);
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = FFMPEGAddress;
            processStartInfo.Arguments = ffmpegCommand;
            FFMPEGManager.RunProcess(processStartInfo);
            string finalVideoAddressForWebcam = Path.Combine(WorkFolderPath, FileName + ".MeetingWithWebcam.flv");

            if (string.IsNullOrEmpty(finalVideoAddress))
            {
                processStartInfo.Arguments = $"-hide_banner -i \"{finalAudioAddress}\" -i \"{webcamVideoFileAddress}\" -map 0:a -map 1:v -c:v copy -c:a copy -shortest -y \"{finalVideoAddressForWebcam}\"";
            }
            else
            {
                string screenShareRes = FFMPEGManager.GetVideosResolution(finalVideoAddress, FFMPEGAddress);
                string webcamRes = FFMPEGManager.GetVideosResolution(webcamVideoFileAddress, FFMPEGAddress);
                int fullWidthRes = int.Parse(screenShareRes.Split('x')[0]) - int.Parse(webcamRes.Split('x')[0]);

                processStartInfo.Arguments = $"-hide_banner -i \"{finalVideoAddress}\"  -i \"{webcamVideoFileAddress}\" " +
                                             $"-filter_complex \"color=s={fullWidthRes + "x" + int.Parse(screenShareRes.Split('x')[1])}:c=black[base]; [0:v] setpts=PTS-STARTPTS[upperleft];[1:v]setpts=PTS-STARTPTS[upperright]; [base][upperleft]overlay=shortest=1[tmp1]; [tmp1][upperright] overlay=shortest=1:x={screenShareRes.Split('x')[0]}\"  -map 0:a -c:a copy -y -shortest \"{finalVideoAddressForWebcam}\"";
            }

            FFMPEGManager.RunProcess(processStartInfo);
            return true;
        }
        else
            return false;
    }

    public bool DownloadAssetsMethod2(string baseUrl, string xmlFileData, List<Cookie> cookies, string outputFolder)
    {
        var baseDownloadAssetUrls = XmlReader.GetDefaultPdfPathForDownload(xmlFileData, baseUrl);

        if (baseDownloadAssetUrls.Count != 0 && cookies.Count != 0)
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

                DownloadSlides(pdfDetail, baseDownloadAddress, cookies);

                if (CancelProcess == true)
                {
                    MessageBox.Show("Process Caneled");
                    return false;
                }

                SwfManager.ConvertSwfToPdf(pdfDetail, SwfFolder);
                counter++;
                Directory.Delete(SwfFolder, true);
                Directory.CreateDirectory(SwfFolder);
            }
            return true;
        }
        else
            return false;
    }

    private string? GetDataForPdf(string defaultAddress)
    {

        var xmlPdfFileNames = defaultAddress + "layout.xml";
        var layoutStreamData = WebManager.GetStreamData(xmlPdfFileNames, Cookies, HttpContentTypeEnum.Xml);
        var response = string.Empty;

        if (layoutStreamData is null)
        {
            return null;
        }

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

            string filePath = Path.Combine(SwfFolder, counter + i + ".swf");

            try
            {
                bool checkProblemWithFile = WebManager.GetStreamData(fileUrl, Cookies, HttpContentTypeEnum.Flash, filePath, true);
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
        VideoManager.FFMPEGManager = FFMPEGManager;
        var videoExportedForConcat = VideoManager.GetVideoLine(filesTime.ScreenStreamData, endTime, ExtractFolder, ExtractFolder, NotAvailableVideoImageAddress, ExtractFolder);

        //var videoLine = VideoManager.GetVideoLineV2(filesTime.ScreenStreamData, endTime, ExtractFolder, videoSize, NotAvailableVideoImageAddress, ExtractFolder);

        var ffmpegCommand = FFMPEGManager.CreateConcatFile(videoExportedForConcat, finalAudioAddress, finalVideoAddress);
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = FFMPEGAddress;
        processStartInfo.Arguments = ffmpegCommand;
        FFMPEGManager.RunProcess(processStartInfo);
    }

    private ListOfStreamData? GetStreamData(string xmlFileData)
    {
        if (string.IsNullOrEmpty(xmlFileData))
        {
            return null;
        }

        //EndRoomTime = XmlReader.FindEndOfTimeV2(xmlFileData);
        var filesTime = XmlReader.FindTimesOfFilesV2(xmlFileData);
        var listOfStreamData = FileManager.CheckFiles(filesTime, ExtractFolder, FFMPEGAddress);
        EndRoomTime = filesTime.Max(x => x.EndFilesTime);

        return listOfStreamData;
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


        if (Directory.Exists(SwfFolder))
            Directory.Delete(SwfFolder, true);
    }

}