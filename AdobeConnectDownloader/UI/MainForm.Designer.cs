namespace AdobeConnectDownloader.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            UrlTextBox = new System.Windows.Forms.TextBox();
            ClearButton = new System.Windows.Forms.Button();
            DataGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            editSaveFolderAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            AddToQueueButton = new System.Windows.Forms.Button();
            DownloadQueueButton = new System.Windows.Forms.Button();
            FileFolderAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            UrlColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ProcessDataGridView = new System.Windows.Forms.DataGridView();
            AddToQueueButtonDesignComponent = new DesignComponent(components);
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            ToolsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            convertFlvvideoToMp4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            convertYourZipFileToVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            convertFlvAudioToMP3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            downloadPdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            zipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            mergeZipFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            DownloadQueueDesignComponent = new DesignComponent(components);
            ClearButtonDesignComponent = new DesignComponent(components);
            DataGridContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ProcessDataGridView).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // UrlTextBox
            // 
            UrlTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            UrlTextBox.BackColor = System.Drawing.SystemColors.Window;
            UrlTextBox.Location = new System.Drawing.Point(12, 35);
            UrlTextBox.Name = "UrlTextBox";
            UrlTextBox.PlaceholderText = "Url : ";
            UrlTextBox.Size = new System.Drawing.Size(601, 23);
            UrlTextBox.TabIndex = 3;
            UrlTextBox.TextChanged += UrlTextBox_TextChanged;
            // 
            // ClearButton
            // 
            ClearButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            ClearButton.BackColor = System.Drawing.Color.FromArgb(23, 165, 137);
            ClearButton.FlatAppearance.BorderSize = 0;
            ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ClearButton.ForeColor = System.Drawing.Color.FromArgb(28, 40, 51);
            ClearButton.Location = new System.Drawing.Point(619, 31);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new System.Drawing.Size(65, 29);
            ClearButton.TabIndex = 7;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = false;
            ClearButton.Click += ClearButton_Click;
            // 
            // DataGridContextMenuStrip
            // 
            DataGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { editSaveFolderAddressToolStripMenuItem, removeToolStripMenuItem });
            DataGridContextMenuStrip.Name = "DataGridContextMenuStrip";
            DataGridContextMenuStrip.Size = new System.Drawing.Size(203, 48);
            // 
            // editSaveFolderAddressToolStripMenuItem
            // 
            editSaveFolderAddressToolStripMenuItem.Name = "editSaveFolderAddressToolStripMenuItem";
            editSaveFolderAddressToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            editSaveFolderAddressToolStripMenuItem.Text = "Edit Save Folder Address";
            editSaveFolderAddressToolStripMenuItem.Click += editSaveFolderAddressToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            removeToolStripMenuItem.Text = "Remove ";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // AddToQueueButton
            // 
            AddToQueueButton.BackColor = System.Drawing.Color.FromArgb(33, 47, 60);
            AddToQueueButton.FlatAppearance.BorderSize = 0;
            AddToQueueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            AddToQueueButton.ForeColor = System.Drawing.Color.White;
            AddToQueueButton.Location = new System.Drawing.Point(143, 64);
            AddToQueueButton.Name = "AddToQueueButton";
            AddToQueueButton.Size = new System.Drawing.Size(108, 36);
            AddToQueueButton.TabIndex = 10;
            AddToQueueButton.Text = "Add To Queue";
            AddToQueueButton.UseVisualStyleBackColor = false;
            AddToQueueButton.Click += AddToQueueButton_Click;
            // 
            // DownloadQueueButton
            // 
            DownloadQueueButton.BackColor = System.Drawing.Color.FromArgb(33, 47, 60);
            DownloadQueueButton.FlatAppearance.BorderSize = 0;
            DownloadQueueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            DownloadQueueButton.ForeColor = System.Drawing.Color.White;
            DownloadQueueButton.Location = new System.Drawing.Point(12, 64);
            DownloadQueueButton.Name = "DownloadQueueButton";
            DownloadQueueButton.Size = new System.Drawing.Size(113, 36);
            DownloadQueueButton.TabIndex = 12;
            DownloadQueueButton.Text = "Download Queue";
            DownloadQueueButton.UseVisualStyleBackColor = false;
            DownloadQueueButton.Click += DownloadQueueButton_Click;
            // 
            // FileFolderAddressColumn
            // 
            FileFolderAddressColumn.HeaderText = "Save Folder";
            FileFolderAddressColumn.Name = "FileFolderAddressColumn";
            FileFolderAddressColumn.ReadOnly = true;
            // 
            // UrlColumn
            // 
            UrlColumn.HeaderText = "Url";
            UrlColumn.Name = "UrlColumn";
            UrlColumn.ReadOnly = true;
            // 
            // ProcessDataGridView
            // 
            ProcessDataGridView.AllowUserToAddRows = false;
            ProcessDataGridView.AllowUserToDeleteRows = false;
            ProcessDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ProcessDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            ProcessDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ProcessDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { UrlColumn, FileFolderAddressColumn });
            ProcessDataGridView.ContextMenuStrip = DataGridContextMenuStrip;
            ProcessDataGridView.Location = new System.Drawing.Point(12, 106);
            ProcessDataGridView.Name = "ProcessDataGridView";
            ProcessDataGridView.ReadOnly = true;
            ProcessDataGridView.RowTemplate.Height = 25;
            ProcessDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            ProcessDataGridView.Size = new System.Drawing.Size(672, 362);
            ProcessDataGridView.TabIndex = 9;
            // 
            // AddToQueueButtonDesignComponent
            // 
            AddToQueueButtonDesignComponent._SetButton = AddToQueueButton;
            AddToQueueButtonDesignComponent.BackGroundHtmlColorCode = "#212F3C";
            AddToQueueButtonDesignComponent.ForeGroundHtmlColorCode = "White";
            AddToQueueButtonDesignComponent.LowLeft = true;
            AddToQueueButtonDesignComponent.LowRight = true;
            AddToQueueButtonDesignComponent.UpLeft = true;
            AddToQueueButtonDesignComponent.UpRight = true;
            AddToQueueButtonDesignComponent.XRadius = 6F;
            AddToQueueButtonDesignComponent.YRadius = 8F;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ToolsStripMenuItem, zipToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(696, 24);
            menuStrip1.TabIndex = 13;
            menuStrip1.Text = "menuStrip1";
            // 
            // ToolsStripMenuItem
            // 
            ToolsStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { convertFlvvideoToMp4ToolStripMenuItem, convertYourZipFileToVideoToolStripMenuItem, convertFlvAudioToMP3ToolStripMenuItem, downloadPdfToolStripMenuItem });
            ToolsStripMenuItem.Name = "ToolsStripMenuItem";
            ToolsStripMenuItem.Size = new System.Drawing.Size(47, 20);
            ToolsStripMenuItem.Text = "Tools";
            // 
            // convertFlvvideoToMp4ToolStripMenuItem
            // 
            convertFlvvideoToMp4ToolStripMenuItem.Name = "convertFlvvideoToMp4ToolStripMenuItem";
            convertFlvvideoToMp4ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            convertFlvvideoToMp4ToolStripMenuItem.Text = "Convert Flv (video) to mp4";
            convertFlvvideoToMp4ToolStripMenuItem.Click += convertFlvvideoToMp4ToolStripMenuItem_Click;
            // 
            // convertYourZipFileToVideoToolStripMenuItem
            // 
            convertYourZipFileToVideoToolStripMenuItem.Name = "convertYourZipFileToVideoToolStripMenuItem";
            convertYourZipFileToVideoToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            convertYourZipFileToVideoToolStripMenuItem.Text = "Convert your zip file to video";
            convertYourZipFileToVideoToolStripMenuItem.Click += convertYourZipFileToVideoToolStripMenuItem_Click;
            // 
            // convertFlvAudioToMP3ToolStripMenuItem
            // 
            convertFlvAudioToMP3ToolStripMenuItem.Name = "convertFlvAudioToMP3ToolStripMenuItem";
            convertFlvAudioToMP3ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            convertFlvAudioToMP3ToolStripMenuItem.Text = "Convert Flv audio to MP3";
            convertFlvAudioToMP3ToolStripMenuItem.Click += convertFlvAudioToMP3ToolStripMenuItem_Click;
            // 
            // downloadPdfToolStripMenuItem
            // 
            downloadPdfToolStripMenuItem.Enabled = false;
            downloadPdfToolStripMenuItem.Name = "downloadPdfToolStripMenuItem";
            downloadPdfToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            downloadPdfToolStripMenuItem.Text = "Download Pdf";
            downloadPdfToolStripMenuItem.Click += downloadPdfToolStripMenuItem_Click;
            // 
            // zipToolStripMenuItem
            // 
            zipToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mergeZipFileToolStripMenuItem });
            zipToolStripMenuItem.Name = "zipToolStripMenuItem";
            zipToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            zipToolStripMenuItem.Text = "Help";
            // 
            // mergeZipFileToolStripMenuItem
            // 
            mergeZipFileToolStripMenuItem.Name = "mergeZipFileToolStripMenuItem";
            mergeZipFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            mergeZipFileToolStripMenuItem.Text = "About Creator";
            mergeZipFileToolStripMenuItem.Click += mergeZipFileToolStripMenuItem_Click;
            // 
            // DownloadQueueDesignComponent
            // 
            DownloadQueueDesignComponent._SetButton = DownloadQueueButton;
            DownloadQueueDesignComponent.BackGroundHtmlColorCode = "#212F3C";
            DownloadQueueDesignComponent.ForeGroundHtmlColorCode = "White";
            DownloadQueueDesignComponent.LowLeft = true;
            DownloadQueueDesignComponent.LowRight = true;
            DownloadQueueDesignComponent.UpLeft = true;
            DownloadQueueDesignComponent.UpRight = true;
            DownloadQueueDesignComponent.XRadius = 7F;
            DownloadQueueDesignComponent.YRadius = 8F;
            // 
            // ClearButtonDesignComponent
            // 
            ClearButtonDesignComponent._SetButton = ClearButton;
            ClearButtonDesignComponent.BackGroundHtmlColorCode = "#17A589";
            ClearButtonDesignComponent.ForeGroundHtmlColorCode = "#1C2833";
            ClearButtonDesignComponent.LowLeft = true;
            ClearButtonDesignComponent.LowRight = true;
            ClearButtonDesignComponent.UpLeft = true;
            ClearButtonDesignComponent.UpRight = true;
            ClearButtonDesignComponent.XRadius = 7F;
            ClearButtonDesignComponent.YRadius = 6F;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(696, 476);
            Controls.Add(menuStrip1);
            Controls.Add(ProcessDataGridView);
            Controls.Add(DownloadQueueButton);
            Controls.Add(AddToQueueButton);
            Controls.Add(ClearButton);
            Controls.Add(UrlTextBox);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Adobe Connect Downloader";
            Load += MainForm_Load;
            DataGridContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ProcessDataGridView).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.TextBox UrlTextBox;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button AddToQueueButton;
        private System.Windows.Forms.ContextMenuStrip DataGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editSaveFolderAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Button DownloadQueueButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFolderAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UrlColumn;
        private System.Windows.Forms.DataGridView ProcessDataGridView;
        private DesignComponent AddToQueueButtonDesignComponent;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertFlvvideoToMp4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeZipFileToolStripMenuItem;
        private DesignComponent DownloadQueueDesignComponent;
        private System.Windows.Forms.ToolStripMenuItem convertYourZipFileToVideoToolStripMenuItem;
        private DesignComponent ClearButtonDesignComponent;
        private System.Windows.Forms.ToolStripMenuItem convertFlvAudioToMP3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadPdfToolStripMenuItem;
    }
}
