using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using AdobeConnectDownloader.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using AdobeConnectDownloader.Application;
using System.Threading.Tasks;
using Octokit;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AdobeConnectDownloader.UI
{
    public partial class MainForm : Form
    {

        public string FFMPEGAddress = Path.Combine(System.Windows.Forms.Application.StartupPath, "Tools", "ffmpeg.exe");
        public string NotAvailableVideoAddress = Path.Combine(System.Windows.Forms.Application.StartupPath, "Not Available Video.png");
        public string SwfFileAddress = Path.Combine(System.Windows.Forms.Application.StartupPath, "Tools", "swfrender.exe");
        public string SwfAddress = Path.Combine(@"C:\\", "Swf Files");
        private SwfManager _swfManager;

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            _swfManager = new SwfManager(SwfFileAddress);

            // check for update
            Task.Run(async () =>
            {
                try
                {
                    var client = new GitHubClient(new Octokit.ProductHeaderValue("AdobeConnectDownloader")); ;
                    var releases = await client.Repository.Release.GetAll("HosseinShams00", "AdobeConnectDownloader");
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Version assemblyVersion = assembly.GetName().Version;
                    var version = int.Parse($"{assemblyVersion.Major}{assemblyVersion.Minor}{assemblyVersion.Build}");
                    var gitVersion = int.Parse(releases[0].TagName.Replace(".", "").Replace("v", ""));

                    if (gitVersion > version)
                    {
                        var openPage = MessageBox.Show("Update available\ndo you want to open download page ?", "Update available", MessageBoxButtons.YesNo);
                        if (openPage == DialogResult.Yes)
                        {
                            WebManager.OpenUrl(releases[0].HtmlUrl);
                        }
                    }
                }
                catch (Exception exception)
                {

                }
            });


        }

        
        private void LinkProcessorButton_Click(object sender, EventArgs e)
        {
            using var newFileData = new AddNewFileForDownloadForm();
            var dialogResult = newFileData.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            using var processForm = ProcessFormMaker(string.Empty, string.Empty, newFileData.Url, newFileData.WorkFolderPath, false, string.Empty);
            this.Hide();
            processForm.ShowDialog();
            this.Show();
        }

        private void AddNewDownloadAddressButton_Click(object sender, EventArgs e)
        {
            using var newFileData = new AddNewFileForDownloadForm();
            var dialogResult = newFileData.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            ProcessDataGridView.Rows.Add(newFileData.FileName, newFileData.LocalZipFile, newFileData.Url, newFileData.WorkFolderPath, newFileData.UserFileType);
        }

        private void editSaveFolderAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProcessDataGridView.SelectedRows.Count == 0)
                return;

            var saveFolderPath = ProcessDataGridView.SelectedRows[0].Cells[1];

            if (saveFolderPath.Value == null)
                return;

            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                saveFolderPath.Value = (object)folderBrowserDialog.SelectedPath;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProcessDataGridView.SelectedRows.Count != 0)
                ProcessDataGridView.Rows.Remove(ProcessDataGridView.SelectedRows[0]);
        }

        private void DownloadQueueButton_Click(object sender, EventArgs e)
        {
            if (ProcessDataGridView.Rows.Count == 0)
                return;

            const string downloadMessage = "Do you want the corresponding file to be generated for each download, or do you want all the files to be downloaded first and then converted to files in order at the end?" +
                                           "\r\nYes = generate the corresponding file for each download" +
                                           "\r\nNo = all files are downloaded first and then converted to files in order at the end" +
                                           "\r\nCancel = cancel downloading files";

            var downloadQuestionResult = MessageBox.Show(downloadMessage, "", MessageBoxButtons.YesNoCancel);
            if (downloadQuestionResult == DialogResult.Cancel)
                return;

            var dataGridViewRows = new List<DataGridViewRow>();
            string workFolderPath;

            switch (downloadQuestionResult)
            {
                case DialogResult.Yes:
                    {
                        for (var i = 0; i < ProcessDataGridView.Rows.Count; i++)
                        {
                            var fileName = (string)ProcessDataGridView.Rows[i].Cells[0].Value;
                            var zipFileAddress = (string?)ProcessDataGridView.Rows[i].Cells[1].Value ?? string.Empty;
                            var url = (string?)ProcessDataGridView.Rows[i].Cells[2].Value;
                            workFolderPath = (string)ProcessDataGridView.Rows[i].Cells[3].Value;
                            var fileType = (FileTypeEnum)ProcessDataGridView.Rows[i].Cells[4].Value;

                            var formTitle = $"{i + 1} / {ProcessDataGridView.Rows.Count}";
                            using var processForm = ProcessFormMaker(formTitle, fileName, url, workFolderPath, false, zipFileAddress);

                            this.Hide();
                            processForm?.ShowDialog();

                            if (processForm.IsEverythingOk == true)
                            {
                                dataGridViewRows.Add(ProcessDataGridView.Rows[i]);
                            }
                            else
                                break;
                        }

                        break;
                    }
                case DialogResult.No:
                    {
                        List<InputFileDetail> completedDownloadFiles = new(dataGridViewRows.Count);

                        for (var i = 0; i < ProcessDataGridView.Rows.Count; i++)
                        {
                            var fileName = (string)ProcessDataGridView.Rows[i].Cells[0].Value;
                            var zipFileAddress = (string?)ProcessDataGridView.Rows[i].Cells[1].Value ?? string.Empty;
                            var url = (string?)ProcessDataGridView.Rows[i].Cells[2].Value;
                            workFolderPath = (string)ProcessDataGridView.Rows[i].Cells[3].Value;
                            var fileType = (FileTypeEnum)ProcessDataGridView.Rows[i].Cells[4].Value;

                            if (fileType == FileTypeEnum.LocalZipFile)
                            {
                                completedDownloadFiles.Add(new()
                                {
                                    FileName = fileName,
                                    Url = null,
                                    WorkFolderPath = workFolderPath,
                                    ZipFileAddress = zipFileAddress
                                });
                                continue;
                            }

                            var formTitle = $"Download {i + 1} / {ProcessDataGridView.Rows.Count} : {fileName}";
                            using var processForm = ProcessFormMaker(formTitle, fileName, url, workFolderPath, true, zipFileAddress);

                            this.Hide();
                            processForm.ShowDialog();
                            zipFileAddress = processForm.ZipFileAddress;

                            if (processForm.IsEverythingOk)
                            {
                                completedDownloadFiles.Add(new()
                                {
                                    FileName = fileName,
                                    Url = null,
                                    WorkFolderPath = workFolderPath,
                                    ZipFileAddress = zipFileAddress
                                });
                            }
                            else
                                break;
                        }

                        if (completedDownloadFiles.Count != ProcessDataGridView.Rows.Count)
                        {
                            this.Show();
                            return;
                        }

                        for (var i = 0; i < completedDownloadFiles.Count; i++)
                        {
                            var fileDetail = completedDownloadFiles[i];
                            var formTitle = $"Append {i + 1} / {ProcessDataGridView.Rows.Count} : {fileDetail.FileName}";

                            using var processForm = ProcessFormMaker(formTitle, fileDetail.FileName, null, fileDetail.WorkFolderPath, false, fileDetail.ZipFileAddress);

                            this.Hide();
                            processForm.ShowDialog();

                            if (processForm.IsEverythingOk)
                            {
                                dataGridViewRows.Add(ProcessDataGridView.Rows[i]);
                            }
                            else
                                break;
                        }

                        break;
                    }
            }
            foreach (var item in dataGridViewRows)
            {
                ProcessDataGridView.Rows.Remove(item);
            }
            this.Show();
        }

        private ProcessForm ProcessFormMaker(string title, string fileName, string? url, string workFolderPath, bool justDownloadFiles, string? zipFileAddress)
        {
            var processForm = new ProcessForm();
            processForm.Title = title;
            processForm.FileName = fileName;
            processForm.FFMPEGAddress = FFMPEGAddress;
            processForm.SwfFileAddress = SwfFileAddress;
            processForm.SwfFolder = SwfAddress;
            processForm.NotAvailableVideoImageAddress = NotAvailableVideoAddress;
            processForm.Url = url;
            processForm.WorkFolderPath = workFolderPath;
            processForm.JustDownloadFiles = justDownloadFiles;
            processForm.ZipFileAddress = zipFileAddress;
            return processForm;
        }

        //private void convertYourZipFileToVideoToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "Choose Zip File | *.zip";
        //    openFileDialog.Title = "Select Zip File";

        //    if (openFileDialog.ShowDialog() != DialogResult.OK)
        //        return;

        //    using var folderBrowserDialog = new FolderBrowserDialog();
        //    folderBrowserDialog.ShowNewFolderButton = true;

        //    if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        //        return;

        //    using var processForm = ProcessFormMaker(string.Empty, string.Empty, null, folderBrowserDialog.SelectedPath, false,
        //        openFileDialog.FileName);

        //    this.Hide();
        //    processForm.ShowDialog();
        //    this.Show();
        //}

        private void convertFlvVideoToMp4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Choose your flv Video|*.flv";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var finalPath = openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 4);
            var command = Application.FFMPEGManager.ConvertFlvVideoToMp4(openFileDialog.FileName, finalPath + ".mp4");
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = FFMPEGAddress;
            processStartInfo.Arguments = command;
            using var process = new Process();
            process.StartInfo = processStartInfo;

            MessageBox.Show("Please don't click on opened page", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            process.Start();
        }

        private void convertFlvAudioToMP3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Choose your flv Audio|*.flv";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var finalPath = openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 4);
            var command = Application.FFMPEGManager.ConvertFlvAudioToMp3(openFileDialog.FileName, finalPath + ".mp3");
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = FFMPEGAddress;
            processStartInfo.Arguments = command;
            using var process = new Process();
            process.StartInfo = processStartInfo;

            MessageBox.Show("Please Dont Click On Opened Page", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            process.Start();
        }

        private void mergeZipFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebManager.OpenUrl("https://github.com/HosseinShams00");
        }

        private async void downloadPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var newFileData = new AddNewFileForDownloadForm();
            newFileData.IsNeedGetFolder = false;
            var dialogResult = newFileData.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            var assetsUrl = WebManager.GetAssetsDownloadUrl(newFileData.Url);
            var cookies = WebManager.GetCookieForm(newFileData.Url, WebManager.GetSessionCookieFrom(newFileData.Url));

            if (cookies.Count == 0)
            {
                MessageBox.Show("Have a problem try again and make sure you login adobe connect server.");
                return;
            }

            using var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.UseDescriptionForTitle = true;
            folderBrowserDialog.Description = "Select folder for save your data";
            var fbdResult = folderBrowserDialog.ShowDialog();
            if (fbdResult is DialogResult.Cancel or DialogResult.No)
                return;

            await Task.Run(() =>
            {

                if (Directory.Exists(SwfAddress) == false)
                    Directory.CreateDirectory(SwfAddress);

                var xmlData = File.ReadAllText(newFileData.FileAddress);

                var baseUrl = newFileData.Url.Substring(0, newFileData.Url.IndexOf("/", 9) + 1);


                WebManager.GetFiles(baseUrl, xmlData, cookies, folderBrowserDialog.SelectedPath);

                var checkAssetsMethod = WebManager.DownloadAssetsMethod1(assetsUrl, cookies, folderBrowserDialog.SelectedPath);


                if (checkAssetsMethod == false)
                {
                    var method2 = DownloadAssetsMethod2(xmlData, newFileData.Url, cookies, folderBrowserDialog.SelectedPath);

                    MessageBox.Show(method2 == true ? "Completed" : "Failed");
                }
                else
                    MessageBox.Show("Completed");

                if (Directory.Exists(SwfAddress))
                    Directory.Delete(SwfAddress, true);

            });



        }

        private bool DownloadAssetsMethod2(string xmlFileData, string url, List<Cookie> cookies, string outputFolder)
        {
            var baseUrl = url.Substring(0, url.IndexOf(".ir/") + 4);
            var baseDownloadAssetUrls = XmlReader.GetDefaultPdfPathForDownload(xmlFileData, baseUrl);

            if (baseDownloadAssetUrls.Count != 0 && cookies.Count != 0)
            {
                var counter = 1;
                foreach (var baseDownloadAddress in baseDownloadAssetUrls)
                {
                    var response = WebManager.GetDataForPdf(baseDownloadAddress, cookies);
                    if (response is null)
                        continue;

                    var pdfDetail = XmlReader.GetPdfDetail(response);
                    pdfDetail.FileName = Path.Combine(outputFolder, $"Pdf {counter}.pdf");

                    DownloadSlides(pdfDetail, baseDownloadAddress, cookies);

                    _swfManager.ConvertSwfToPdf(pdfDetail, SwfAddress);
                    counter++;
                }
                return true;
            }
            else
                return false;
        }

        private void DownloadSlides(PdfDetail pdfDetail, string baseUrlAddressForDownload, List<Cookie> Cookies)
        {
            for (var i = 1; i <= pdfDetail.PageNumber; i++)
            {
                var fileUrl = baseUrlAddressForDownload + i + ".swf";

                var counter = "";
                for (var j = 0; j < pdfDetail.PageNumber.ToString().Length - i.ToString().Length; j++)
                    counter += "0";

                var filePath = Path.Combine(SwfAddress, counter + i + ".swf");

                try
                {
                    var checkProblemWithFile = WebManager.GetStreamData(fileUrl, Cookies, WebManager.HttpContentType.Flash, filePath, true);
                    if (checkProblemWithFile)
                        continue;
                    MessageBox.Show("We Have Problem Try Again");
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
