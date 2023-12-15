using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdobeConnectDownloader.UI
{
    public partial class AddNewFileForDownloadForm : Form
    {
        public bool IsNeedGetFolder { get; set; } = true;
        public string? Url { get; set; } = null;
        public string? LocalZipFile { get; set; }
        public string WorkFolderPath { get; set; }
        public string FileAddress { get; set; }
        public string FileName { get; set; }
        public FileTypeEnum UserFileType { get; set; }
        public AddNewFileForDownloadForm()
        {
            InitializeComponent();
        }

        private void UrlTextBox_TextChanged(object sender, EventArgs e)
        {
            if ((UrlOrPathFileTextBox.Text.Trim().StartsWith("http://") || UrlOrPathFileTextBox.Text.Trim().StartsWith("https://")) == false)
            {
                UserFileType = FileTypeEnum.LocalZipFile;
                return;
            }

            SubmitButton.Enabled = string.IsNullOrEmpty(UrlOrPathFileTextBox.Text.Trim()) == false && string.IsNullOrEmpty(SaveInTextBox.Text.Trim()) == false;
            Url = UrlOrPathFileTextBox.Text.Trim();
        }

        private void SaveInTextBox_TextChanged(object sender, EventArgs e)
        {
            SubmitButton.Enabled = string.IsNullOrEmpty(UrlOrPathFileTextBox.Text.Trim()) == false &&
                                   string.IsNullOrEmpty(SaveInTextBox.Text.Trim()) == false;
        }

        private void SaveDialogButton_Click(object sender, EventArgs e)
        {
            if (IsNeedGetFolder)
            {
                using var folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveInTextBox.Text = folderBrowserDialog.SelectedPath;
                    WorkFolderPath = folderBrowserDialog.SelectedPath;
                }
            }
            else
            {
                SaveFolderLabel.Text = "Zip File : ";
                using var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "choose indexstream.xml  | *.xml";
                openFileDialog.Title = "select indexstream.xml from your downloaded zip file";
                var ofdResult = openFileDialog.ShowDialog();

                if (ofdResult != DialogResult.OK)
                    return;
                SaveInTextBox.Text = openFileDialog.FileName;
                FileAddress = openFileDialog.FileName;
                UserFileType = FileTypeEnum.Xml;
            }

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            FileName = FileNameTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UrlOrPathFileButton_Click(object sender, EventArgs e)
        {

            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "choose your meeting file  | *.zip";
            openFileDialog.Title = "select meeting zip file";
            var ofdResult = openFileDialog.ShowDialog();

            if (ofdResult != DialogResult.OK)
                return;

            UrlOrPathFileTextBox.Text = openFileDialog.FileName;
            FileAddress = openFileDialog.FileName;
            UserFileType = FileTypeEnum.LocalZipFile;
            LocalZipFile = FileAddress;

        }

        private void AddNewFileForDownloadForm_Load(object sender, EventArgs e)
        {
            if (IsNeedGetFolder == false)
            {
                FileNameTextBox.Enabled = false;
                UrlOrPathFileLabel.Text = "Url : ";
                UrlOrPathFileButton.Visible = false;
            }
        }
    }

    public enum FileTypeEnum
    {
        LocalZipFile,
        Xml,
        Url
    }
}
