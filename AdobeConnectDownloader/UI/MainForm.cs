using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AdobeConnectDownloader.UI
{
    public partial class MainForm : Form
    {

        public string FFMPEGAddress = Path.Combine(System.Windows.Forms.Application.StartupPath, "Tools", "ffmpeg.exe");
        public string NotAvailableVideoAddress = Path.Combine(System.Windows.Forms.Application.StartupPath, "Not Avaliable Video.png");

        public MainForm()
        {
            InitializeComponent();
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

            DataGridView copyDataGridView = ProcessDataGridView;
            if (MessageBox.Show("Are you sure to download Queue list ? ", "Adobe Downloader", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = 0; i < ProcessDataGridView.Rows.Count; i++)
                {
                    string url = (string)ProcessDataGridView.Rows[i].Cells[0].Value;
                    string WorkFolderPath = (string)ProcessDataGridView.Rows[i].Cells[1].Value;

                    ProcessForm processForm = new ProcessForm();
                    processForm.Title = $"{i + 1} / {ProcessDataGridView.Rows.Count}";
                    processForm.FFMPEGAddress = FFMPEGAddress;
                    processForm.NotAvailableVideoImageAddress = NotAvailableVideoAddress;
                    processForm.Url = url;
                    processForm.WorkFolderPath = WorkFolderPath;
                    this.Hide();
                    processForm.ShowDialog();

                    if (processForm.IsEverythingOk == true)
                    {
                        copyDataGridView.Rows.Remove(ProcessDataGridView.Rows[i]);
                    }
                    else if (processForm.CancelProcess == true)
                    {
                        break;
                    }

                }

            }
            ProcessDataGridView = copyDataGridView;
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

        private void convertFlvvideosToMp4ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
