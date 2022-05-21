using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using AdobeConnectDownloader.Model;
using System.Collections.Generic;
using System.Net;
using AdobeConnectDownloader.Application;
using System.Threading.Tasks;
using System.Web;

namespace AdobeConnectDownloader.UI
{
    public partial class MainForm : Form
    {

        public string FFMPEGAddress = Path.Combine(System.Windows.Forms.Application.StartupPath, "Tools", "ffmpeg.exe");
        public string NotAvailableVideoAddress = Path.Combine(System.Windows.Forms.Application.StartupPath, "Not Avaliable Video.png");
        public string SwfFileAddress = Path.Combine(System.Windows.Forms.Application.StartupPath, "Tools", "swfrender.exe");
        public string SwfAddress = Path.Combine(@"C:\\", "Swf Files");
        private SwfManager SwfManager;

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            SwfManager = new SwfManager(SwfFileAddress);
        }

        private void LinkProcessorButton_Click(object sender, EventArgs e)
        {
            ProcessForm processForm = new ProcessForm();
            processForm.Url = UrlTextBox.Text.Trim();
            processForm.FFMPEGAddress = FFMPEGAddress;
            processForm.NotAvailableVideoImageAddress = NotAvailableVideoAddress;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                processForm.WorkFolderPath = folderBrowserDialog.SelectedPath;

                this.Hide();
                processForm.ShowDialog();
                this.Show();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            UrlTextBox.Text = String.Empty;
        }

        private void AddToQueueButton_Click(object sender, EventArgs e)
        {
            if (UrlTextBox.Text.Trim() != "" || UrlTextBox.Text.Trim() != String.Empty)
            {
                ProcessDataGridView.Rows.Add(UrlTextBox.Text, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                UrlTextBox.Text = String.Empty;
            }
        }

        private void editSaveFolderAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProcessDataGridView.SelectedRows.Count != 0)
            {
                var saveFolderPath = ProcessDataGridView.SelectedRows[0].Cells[1];
                if (saveFolderPath.Value != null)
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        saveFolderPath.Value = (object)folderBrowserDialog.SelectedPath;
                    }
                }
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

            List<DataGridViewRow> dataGridViewRows = new List<DataGridViewRow>();

            if (MessageBox.Show("Are you sure to download Queue list ? ", "Adobe Downloader", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = 0; i < ProcessDataGridView.Rows.Count; i++)
                {
                    string url = (string)ProcessDataGridView.Rows[i].Cells[0].Value;
                    string WorkFolderPath = (string)ProcessDataGridView.Rows[i].Cells[1].Value;

                    ProcessForm processForm = new ProcessForm();
                    processForm.Title = $"{i + 1} / {ProcessDataGridView.Rows.Count}";
                    processForm.FFMPEGAddress = FFMPEGAddress;
                    processForm.SwfFileAddress = SwfFileAddress;
                    processForm.swfFolder = SwfAddress;
                    processForm.NotAvailableVideoImageAddress = NotAvailableVideoAddress;
                    processForm.Url = url;
                    processForm.WorkFolderPath = WorkFolderPath;
                    this.Hide();
                    processForm.ShowDialog();

                    if (processForm.IsEverythingOk == true)
                    {
                        dataGridViewRows.Add(ProcessDataGridView.Rows[i]);
                    }
                    else
                        break;

                }

            }

            foreach (var item in dataGridViewRows)
            {
                ProcessDataGridView.Rows.Remove(item);
            }
            this.Show();
        }

        private void convertYourZipFileToVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Choose Zip File | *.zip";
            openFileDialog.Title = "Select Zip File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ProcessForm processForm = new ProcessForm();
                processForm.FFMPEGAddress = FFMPEGAddress;
                processForm.NotAvailableVideoImageAddress = NotAvailableVideoAddress;
                processForm.SwfFileAddress = SwfFileAddress;
                processForm.swfFolder = SwfAddress;
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowNewFolderButton = true;


                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    processForm.WorkFolderPath = folderBrowserDialog.SelectedPath;
                    processForm.ZipFileAddress = openFileDialog.FileName;

                    this.Hide();

                    processForm.ShowDialog();
                    this.Show();
                }
            }
        }

        private void convertFlvvideoToMp4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Choose your flv Video|*.flv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string finalPath = openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 4);
                string command = Application.FFMPEGManager.ConvertFlvVideoToMp4(openFileDialog.FileName, finalPath + ".mp4");
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = FFMPEGAddress;
                processStartInfo.Arguments = command;
                Process process = new Process();
                process.StartInfo = processStartInfo;

                MessageBox.Show("Please Dont Click On Opened Page", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                process.Start();
            }
        }

        private void convertFlvAudioToMP3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Choose your flv Audio|*.flv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string finalPath = openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 4);
                string command = Application.FFMPEGManager.ConvertFlvAudioToMp3(openFileDialog.FileName, finalPath + ".mp3");
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = FFMPEGAddress;
                processStartInfo.Arguments = command;
                Process process = new Process();
                process.StartInfo = processStartInfo;

                MessageBox.Show("Please Dont Click On Opened Page", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                process.Start();
            }
        }

        private void mergeZipFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Open This Github Page : https://github.com/HosseinShams00");
        }

        private async void downloadPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UrlTextBox.Enabled = false;
            string url = UrlTextBox.Text.Trim();
            UrlTextBox.Text = null;

            string assetsUrl = WebManager.GetAssetsDownloadUrl(url);
            var cookies = WebManager.GetCookieForm(url, WebManager.GetSessionCookieFrom(url));

            if (cookies.Count == 0)
            {
                MessageBox.Show("Have a problem try again and make sure you login adobe connect server.");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "choose indexstream.xml  | *.xml";
            openFileDialog.Title = "select indexstream.xml from your downloaded zip file";
            var ofdResult = openFileDialog.ShowDialog();
            if (ofdResult == DialogResult.Cancel || ofdResult == DialogResult.No)
                return;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.UseDescriptionForTitle = true;
            folderBrowserDialog.Description = "Select for for save yout data";
            var fbdResult = folderBrowserDialog.ShowDialog();
            if (fbdResult == DialogResult.Cancel || fbdResult == DialogResult.No)
                return;

            await Task.Run(() =>
            {

                if (Directory.Exists(SwfAddress) == false)
                    Directory.CreateDirectory(SwfAddress);

                string xmlData = File.ReadAllText(openFileDialog.FileName);

                string baseUrl = url.Substring(0, url.IndexOf("/", 9) + 1);


                WebManager.GetFiles(baseUrl, xmlData, cookies, folderBrowserDialog.SelectedPath);

                var checkAssetsMethod = WebManager.DownloadAssetsMethod1(assetsUrl, cookies, folderBrowserDialog.SelectedPath);


                if (checkAssetsMethod == false)
                {

                    var method2 = DownloadAssetsMethod2(xmlData, url, cookies, folderBrowserDialog.SelectedPath);

                    if (method2 == true)
                        MessageBox.Show("Completed");
                    else
                        MessageBox.Show("Faild");

                }
                else
                    MessageBox.Show("Completed");

                if (Directory.Exists(SwfAddress))
                    Directory.Delete(SwfAddress, true);

            });

            UrlTextBox.Enabled = true;


        }
        private void UrlTextBox_TextChanged(object sender, EventArgs e)
        {
            if (UrlTextBox.Text.Trim().StartsWith("http://") || UrlTextBox.Text.Trim().StartsWith("https://"))
                downloadPdfToolStripMenuItem.Enabled = true;
            else
                downloadPdfToolStripMenuItem.Enabled = false;
        }

        private bool DownloadAssetsMethod2(string xmlFileData, string url, List<Cookie> cookies, string outputFolder)
        {
            string baseUrl = url.Substring(0, url.IndexOf(".ir/") + 4);
            var baseDownloadAssetUrls = XmlReader.GetDefaultPdfPathForDownload(xmlFileData, baseUrl);

            if (baseDownloadAssetUrls.Count != 0 && cookies.Count != 0)
            {
                int couner = 1;
                foreach (var baseDownloadAddress in baseDownloadAssetUrls)
                {
                    string response = WebManager.GetDataForPdf(baseDownloadAddress, cookies);
                    if (response == null)
                        continue;

                    var pdfDetail = XmlReader.GetPdfDetail(response);
                    pdfDetail.FileName = Path.Combine(outputFolder, $"Pdf {couner}.pdf");

                    DownloadSlides(pdfDetail, baseDownloadAddress, cookies);

                    SwfManager.ConvertSwfToPdf(pdfDetail, SwfAddress);
                    couner++;
                }
                return true;
            }
            else
                return false;
        }

        private void DownloadSlides(PdfDetail pdfDetail, string baseUrlAddressForDownload, List<Cookie> Cookies)
        {
            for (int i = 1; i <= pdfDetail.PageNumber; i++)
            {
                string fileUrl = baseUrlAddressForDownload + i + ".swf";

                string counter = "";
                for (int j = 0; j < pdfDetail.PageNumber.ToString().Length - i.ToString().Length; j++)
                    counter += "0";

                string filePath = Path.Combine(SwfAddress, counter + i + ".swf");

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

    }
}
