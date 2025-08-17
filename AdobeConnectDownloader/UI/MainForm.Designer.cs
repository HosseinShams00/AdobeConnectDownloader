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
            DataGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            editSaveFolderAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            AddToQueueButton = new System.Windows.Forms.Button();
            ProcessQueueButton = new System.Windows.Forms.Button();
            ProcessDataGridView = new System.Windows.Forms.DataGridView();
            FileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ZipFileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            UrlColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            FileFolderAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            FileTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            AddToQueueButtonDesignComponent = new DesignComponent(components);
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            ToolsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            convertFlvvideoToMp4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            convertFlvAudioToMP3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            downloadPdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            zipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            mergeZipFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            DownloadQueueDesignComponent = new DesignComponent(components);
            openWebBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            DataGridContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ProcessDataGridView).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // DataGridContextMenuStrip
            // 
            DataGridContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            DataGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { editSaveFolderAddressToolStripMenuItem, removeToolStripMenuItem });
            DataGridContextMenuStrip.Name = "DataGridContextMenuStrip";
            DataGridContextMenuStrip.Size = new System.Drawing.Size(243, 52);
            // 
            // editSaveFolderAddressToolStripMenuItem
            // 
            editSaveFolderAddressToolStripMenuItem.Name = "editSaveFolderAddressToolStripMenuItem";
            editSaveFolderAddressToolStripMenuItem.Size = new System.Drawing.Size(242, 24);
            editSaveFolderAddressToolStripMenuItem.Text = "Edit Save Folder Address";
            editSaveFolderAddressToolStripMenuItem.Click += editSaveFolderAddressToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new System.Drawing.Size(242, 24);
            removeToolStripMenuItem.Text = "Remove ";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // AddToQueueButton
            // 
            AddToQueueButton.BackColor = System.Drawing.Color.FromArgb(33, 47, 60);
            AddToQueueButton.FlatAppearance.BorderSize = 0;
            AddToQueueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            AddToQueueButton.ForeColor = System.Drawing.Color.White;
            AddToQueueButton.Location = new System.Drawing.Point(689, 36);
            AddToQueueButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            AddToQueueButton.Name = "AddToQueueButton";
            AddToQueueButton.Size = new System.Drawing.Size(93, 48);
            AddToQueueButton.TabIndex = 10;
            AddToQueueButton.Text = "Add +";
            AddToQueueButton.UseVisualStyleBackColor = false;
            AddToQueueButton.Click += AddNewDownloadAddressButton_Click;
            // 
            // ProcessQueueButton
            // 
            ProcessQueueButton.BackColor = System.Drawing.Color.FromArgb(33, 47, 60);
            ProcessQueueButton.FlatAppearance.BorderSize = 0;
            ProcessQueueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ProcessQueueButton.ForeColor = System.Drawing.Color.White;
            ProcessQueueButton.Location = new System.Drawing.Point(14, 36);
            ProcessQueueButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            ProcessQueueButton.Name = "ProcessQueueButton";
            ProcessQueueButton.Size = new System.Drawing.Size(129, 48);
            ProcessQueueButton.TabIndex = 12;
            ProcessQueueButton.Text = "Process Queue";
            ProcessQueueButton.UseVisualStyleBackColor = false;
            ProcessQueueButton.Click += ProcessQueueButtonClick;
            // 
            // ProcessDataGridView
            // 
            ProcessDataGridView.AllowUserToAddRows = false;
            ProcessDataGridView.AllowUserToDeleteRows = false;
            ProcessDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ProcessDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            ProcessDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ProcessDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { FileNameColumn, ZipFileColumn, UrlColumn, FileFolderAddressColumn, FileTypeColumn });
            ProcessDataGridView.ContextMenuStrip = DataGridContextMenuStrip;
            ProcessDataGridView.Location = new System.Drawing.Point(14, 92);
            ProcessDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            ProcessDataGridView.Name = "ProcessDataGridView";
            ProcessDataGridView.ReadOnly = true;
            ProcessDataGridView.RowHeadersWidth = 51;
            ProcessDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            ProcessDataGridView.Size = new System.Drawing.Size(768, 532);
            ProcessDataGridView.TabIndex = 9;
            // 
            // FileNameColumn
            // 
            FileNameColumn.HeaderText = "File Name";
            FileNameColumn.MinimumWidth = 6;
            FileNameColumn.Name = "FileNameColumn";
            FileNameColumn.ReadOnly = true;
            // 
            // ZipFileColumn
            // 
            ZipFileColumn.HeaderText = "Zip File";
            ZipFileColumn.MinimumWidth = 6;
            ZipFileColumn.Name = "ZipFileColumn";
            ZipFileColumn.ReadOnly = true;
            // 
            // UrlColumn
            // 
            UrlColumn.HeaderText = "Url";
            UrlColumn.MinimumWidth = 6;
            UrlColumn.Name = "UrlColumn";
            UrlColumn.ReadOnly = true;
            // 
            // FileFolderAddressColumn
            // 
            FileFolderAddressColumn.HeaderText = "Save Folder";
            FileFolderAddressColumn.MinimumWidth = 6;
            FileFolderAddressColumn.Name = "FileFolderAddressColumn";
            FileFolderAddressColumn.ReadOnly = true;
            // 
            // FileTypeColumn
            // 
            FileTypeColumn.HeaderText = "FileTypeColumn";
            FileTypeColumn.MinimumWidth = 6;
            FileTypeColumn.Name = "FileTypeColumn";
            FileTypeColumn.ReadOnly = true;
            FileTypeColumn.Visible = false;
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
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ToolsStripMenuItem, zipToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            menuStrip1.Size = new System.Drawing.Size(795, 30);
            menuStrip1.TabIndex = 13;
            menuStrip1.Text = "menuStrip1";
            // 
            // ToolsStripMenuItem
            // 
            ToolsStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { convertFlvvideoToMp4ToolStripMenuItem, convertFlvAudioToMP3ToolStripMenuItem, downloadPdfToolStripMenuItem, openWebBrowserToolStripMenuItem });
            ToolsStripMenuItem.Name = "ToolsStripMenuItem";
            ToolsStripMenuItem.Size = new System.Drawing.Size(58, 24);
            ToolsStripMenuItem.Text = "Tools";
            // 
            // convertFlvvideoToMp4ToolStripMenuItem
            // 
            convertFlvvideoToMp4ToolStripMenuItem.Name = "convertFlvvideoToMp4ToolStripMenuItem";
            convertFlvvideoToMp4ToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            convertFlvvideoToMp4ToolStripMenuItem.Text = "Convert Flv (video) to mp4";
            convertFlvvideoToMp4ToolStripMenuItem.Click += convertFlvVideoToMp4ToolStripMenuItem_Click;
            // 
            // convertFlvAudioToMP3ToolStripMenuItem
            // 
            convertFlvAudioToMP3ToolStripMenuItem.Name = "convertFlvAudioToMP3ToolStripMenuItem";
            convertFlvAudioToMP3ToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            convertFlvAudioToMP3ToolStripMenuItem.Text = "Convert Flv audio to MP3";
            convertFlvAudioToMP3ToolStripMenuItem.Click += convertFlvAudioToMP3ToolStripMenuItem_Click;
            // 
            // downloadPdfToolStripMenuItem
            // 
            downloadPdfToolStripMenuItem.Name = "downloadPdfToolStripMenuItem";
            downloadPdfToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            downloadPdfToolStripMenuItem.Text = "Download Pdf";
            downloadPdfToolStripMenuItem.Click += downloadPdfToolStripMenuItem_Click;
            // 
            // zipToolStripMenuItem
            // 
            zipToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mergeZipFileToolStripMenuItem });
            zipToolStripMenuItem.Name = "zipToolStripMenuItem";
            zipToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            zipToolStripMenuItem.Text = "Help";
            // 
            // mergeZipFileToolStripMenuItem
            // 
            mergeZipFileToolStripMenuItem.Name = "mergeZipFileToolStripMenuItem";
            mergeZipFileToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            mergeZipFileToolStripMenuItem.Text = "About Creator";
            mergeZipFileToolStripMenuItem.Click += mergeZipFileToolStripMenuItem_Click;
            // 
            // DownloadQueueDesignComponent
            // 
            DownloadQueueDesignComponent._SetButton = ProcessQueueButton;
            DownloadQueueDesignComponent.BackGroundHtmlColorCode = "#212F3C";
            DownloadQueueDesignComponent.ForeGroundHtmlColorCode = "White";
            DownloadQueueDesignComponent.LowLeft = true;
            DownloadQueueDesignComponent.LowRight = true;
            DownloadQueueDesignComponent.UpLeft = true;
            DownloadQueueDesignComponent.UpRight = true;
            DownloadQueueDesignComponent.XRadius = 7F;
            DownloadQueueDesignComponent.YRadius = 8F;
            // 
            // openWebBrowserToolStripMenuItem
            // 
            openWebBrowserToolStripMenuItem.Name = "openWebBrowserToolStripMenuItem";
            openWebBrowserToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            openWebBrowserToolStripMenuItem.Text = "Open Web Browser";
            openWebBrowserToolStripMenuItem.Click += openWebBrowserToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(795, 635);
            Controls.Add(menuStrip1);
            Controls.Add(ProcessDataGridView);
            Controls.Add(ProcessQueueButton);
            Controls.Add(AddToQueueButton);
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        private System.Windows.Forms.Button AddToQueueButton;
        private System.Windows.Forms.ContextMenuStrip DataGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editSaveFolderAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Button ProcessQueueButton;
        private System.Windows.Forms.DataGridView ProcessDataGridView;
        private DesignComponent AddToQueueButtonDesignComponent;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertFlvvideoToMp4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeZipFileToolStripMenuItem;
        private DesignComponent DownloadQueueDesignComponent;
        private System.Windows.Forms.ToolStripMenuItem convertFlvAudioToMP3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadPdfToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZipFileColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UrlColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFolderAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileTypeColumn;
        private System.Windows.Forms.ToolStripMenuItem openWebBrowserToolStripMenuItem;
    }
}
