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
        public string Url { get; set; }
        public string WorkFolderPath { get; set; }
        public string FileAddress { get; set; }
        public AddNewFileForDownloadForm()
        {
            InitializeComponent();
        }

        private void UrlTextBox_TextChanged(object sender, EventArgs e)
        {
            if ((UrlTextBox.Text.Trim().StartsWith("http://") || UrlTextBox.Text.Trim().StartsWith("https://")) == false)
                return;

            SubmitButton.Enabled = string.IsNullOrEmpty(UrlTextBox.Text.Trim()) == false && string.IsNullOrEmpty(SaveInTextBox.Text.Trim()) == false;
            Url = UrlTextBox.Text.Trim();
        }

        private void SaveInTextBox_TextChanged(object sender, EventArgs e)
        {
            SubmitButton.Enabled = ((UrlTextBox.Text.Trim().StartsWith("http://") || UrlTextBox.Text.Trim().StartsWith("https://")) 
                                    && string.IsNullOrEmpty(UrlTextBox.Text.Trim()) == false )
                                    && string.IsNullOrEmpty(SaveInTextBox.Text.Trim()) == false;
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
            }

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
