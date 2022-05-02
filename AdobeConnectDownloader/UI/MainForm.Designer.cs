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
            this.components = new System.ComponentModel.Container();
            this.UrlTextBox = new System.Windows.Forms.TextBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.DataGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editSaveFolderAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToQueueButton = new System.Windows.Forms.Button();
            this.DownloadQueueButton = new System.Windows.Forms.Button();
            this.FileFolderAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UrlColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessDataGridView = new System.Windows.Forms.DataGridView();
            this.AddToQueueButtonDesignComponent = new AdobeConnectDownloader.UI.DesignComponent(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertFlvvideosToMp4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertYourZipFileToVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertFlvAudioToMP3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadPdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeZipFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadQueueDesignComponent = new AdobeConnectDownloader.UI.DesignComponent(this.components);
            this.ClearButtonDesignComponent = new AdobeConnectDownloader.UI.DesignComponent(this.components);
            this.DataGridContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UrlTextBox
            // 
            this.UrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UrlTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.UrlTextBox.Location = new System.Drawing.Point(12, 35);
            this.UrlTextBox.Name = "UrlTextBox";
            this.UrlTextBox.PlaceholderText = "Url : ";
            this.UrlTextBox.Size = new System.Drawing.Size(601, 23);
            this.UrlTextBox.TabIndex = 3;
            this.UrlTextBox.TextChanged += new System.EventHandler(this.UrlTextBox_TextChanged);
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(165)))), ((int)(((byte)(137)))));
            this.ClearButton.FlatAppearance.BorderSize = 0;
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.ClearButton.Location = new System.Drawing.Point(619, 31);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(65, 29);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // DataGridContextMenuStrip
            // 
            this.DataGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSaveFolderAddressToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.DataGridContextMenuStrip.Name = "DataGridContextMenuStrip";
            this.DataGridContextMenuStrip.Size = new System.Drawing.Size(203, 48);
            // 
            // editSaveFolderAddressToolStripMenuItem
            // 
            this.editSaveFolderAddressToolStripMenuItem.Name = "editSaveFolderAddressToolStripMenuItem";
            this.editSaveFolderAddressToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.editSaveFolderAddressToolStripMenuItem.Text = "Edit Save Folder Address";
            this.editSaveFolderAddressToolStripMenuItem.Click += new System.EventHandler(this.editSaveFolderAddressToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.removeToolStripMenuItem.Text = "Remove ";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // AddToQueueButton
            // 
            this.AddToQueueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(47)))), ((int)(((byte)(60)))));
            this.AddToQueueButton.FlatAppearance.BorderSize = 0;
            this.AddToQueueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddToQueueButton.ForeColor = System.Drawing.Color.White;
            this.AddToQueueButton.Location = new System.Drawing.Point(143, 64);
            this.AddToQueueButton.Name = "AddToQueueButton";
            this.AddToQueueButton.Size = new System.Drawing.Size(108, 36);
            this.AddToQueueButton.TabIndex = 10;
            this.AddToQueueButton.Text = "Add To Queue";
            this.AddToQueueButton.UseVisualStyleBackColor = false;
            this.AddToQueueButton.Click += new System.EventHandler(this.AddToQueueButton_Click);
            // 
            // DownloadQueueButton
            // 
            this.DownloadQueueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(47)))), ((int)(((byte)(60)))));
            this.DownloadQueueButton.FlatAppearance.BorderSize = 0;
            this.DownloadQueueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadQueueButton.ForeColor = System.Drawing.Color.White;
            this.DownloadQueueButton.Location = new System.Drawing.Point(12, 64);
            this.DownloadQueueButton.Name = "DownloadQueueButton";
            this.DownloadQueueButton.Size = new System.Drawing.Size(113, 36);
            this.DownloadQueueButton.TabIndex = 12;
            this.DownloadQueueButton.Text = "Download Queue";
            this.DownloadQueueButton.UseVisualStyleBackColor = false;
            this.DownloadQueueButton.Click += new System.EventHandler(this.DownloadQueueButton_Click);
            // 
            // FileFolderAddressColumn
            // 
            this.FileFolderAddressColumn.HeaderText = "Save Folder";
            this.FileFolderAddressColumn.Name = "FileFolderAddressColumn";
            this.FileFolderAddressColumn.ReadOnly = true;
            // 
            // UrlColumn
            // 
            this.UrlColumn.HeaderText = "Url";
            this.UrlColumn.Name = "UrlColumn";
            this.UrlColumn.ReadOnly = true;
            // 
            // ProcessDataGridView
            // 
            this.ProcessDataGridView.AllowUserToAddRows = false;
            this.ProcessDataGridView.AllowUserToDeleteRows = false;
            this.ProcessDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProcessDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProcessDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UrlColumn,
            this.FileFolderAddressColumn});
            this.ProcessDataGridView.ContextMenuStrip = this.DataGridContextMenuStrip;
            this.ProcessDataGridView.Location = new System.Drawing.Point(12, 106);
            this.ProcessDataGridView.Name = "ProcessDataGridView";
            this.ProcessDataGridView.ReadOnly = true;
            this.ProcessDataGridView.RowTemplate.Height = 25;
            this.ProcessDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProcessDataGridView.Size = new System.Drawing.Size(672, 362);
            this.ProcessDataGridView.TabIndex = 9;
            // 
            // AddToQueueButtonDesignComponent
            // 
            this.AddToQueueButtonDesignComponent._SetButton = this.AddToQueueButton;
            this.AddToQueueButtonDesignComponent.BackGroundHtmlColorCode = "#212F3C";
            this.AddToQueueButtonDesignComponent.ForeGroundHtmlColorCode = "White";
            this.AddToQueueButtonDesignComponent.LowLeft = true;
            this.AddToQueueButtonDesignComponent.LowRight = true;
            this.AddToQueueButtonDesignComponent.UpLeft = true;
            this.AddToQueueButtonDesignComponent.UpRight = true;
            this.AddToQueueButtonDesignComponent.XRadius = 6F;
            this.AddToQueueButtonDesignComponent.YRadius = 8F;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolsStripMenuItem,
            this.zipToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(696, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolsStripMenuItem
            // 
            this.ToolsStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertFlvvideosToMp4ToolStripMenuItem,
            this.convertYourZipFileToVideoToolStripMenuItem,
            this.convertFlvAudioToMP3ToolStripMenuItem,
            this.downloadPdfToolStripMenuItem});
            this.ToolsStripMenuItem.Name = "ToolsStripMenuItem";
            this.ToolsStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.ToolsStripMenuItem.Text = "Tools";
            // 
            // convertFlvvideosToMp4ToolStripMenuItem
            // 
            this.convertFlvvideosToMp4ToolStripMenuItem.Name = "convertFlvvideosToMp4ToolStripMenuItem";
            this.convertFlvvideosToMp4ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.convertFlvvideosToMp4ToolStripMenuItem.Text = "Convert Flv (videos) to mp4";
            this.convertFlvvideosToMp4ToolStripMenuItem.Click += new System.EventHandler(this.convertFlvvideosToMp4ToolStripMenuItem_Click);
            // 
            // convertYourZipFileToVideoToolStripMenuItem
            // 
            this.convertYourZipFileToVideoToolStripMenuItem.Name = "convertYourZipFileToVideoToolStripMenuItem";
            this.convertYourZipFileToVideoToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.convertYourZipFileToVideoToolStripMenuItem.Text = "Convert your zip file to video";
            this.convertYourZipFileToVideoToolStripMenuItem.Click += new System.EventHandler(this.convertYourZipFileToVideoToolStripMenuItem_Click);
            // 
            // convertFlvAudioToMP3ToolStripMenuItem
            // 
            this.convertFlvAudioToMP3ToolStripMenuItem.Name = "convertFlvAudioToMP3ToolStripMenuItem";
            this.convertFlvAudioToMP3ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.convertFlvAudioToMP3ToolStripMenuItem.Text = "Convert Flv audio to MP3";
            this.convertFlvAudioToMP3ToolStripMenuItem.Click += new System.EventHandler(this.convertFlvAudioToMP3ToolStripMenuItem_Click);
            // 
            // downloadPdfToolStripMenuItem
            // 
            this.downloadPdfToolStripMenuItem.Enabled = false;
            this.downloadPdfToolStripMenuItem.Name = "downloadPdfToolStripMenuItem";
            this.downloadPdfToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.downloadPdfToolStripMenuItem.Text = "Download Pdf";
            this.downloadPdfToolStripMenuItem.Click += new System.EventHandler(this.downloadPdfToolStripMenuItem_Click);
            // 
            // zipToolStripMenuItem
            // 
            this.zipToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mergeZipFileToolStripMenuItem});
            this.zipToolStripMenuItem.Name = "zipToolStripMenuItem";
            this.zipToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.zipToolStripMenuItem.Text = "Help";
            // 
            // mergeZipFileToolStripMenuItem
            // 
            this.mergeZipFileToolStripMenuItem.Name = "mergeZipFileToolStripMenuItem";
            this.mergeZipFileToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.mergeZipFileToolStripMenuItem.Text = "About Creator";
            this.mergeZipFileToolStripMenuItem.Click += new System.EventHandler(this.mergeZipFileToolStripMenuItem_Click);
            // 
            // DownloadQueueDesignComponent
            // 
            this.DownloadQueueDesignComponent._SetButton = this.DownloadQueueButton;
            this.DownloadQueueDesignComponent.BackGroundHtmlColorCode = "#212F3C";
            this.DownloadQueueDesignComponent.ForeGroundHtmlColorCode = "White";
            this.DownloadQueueDesignComponent.LowLeft = true;
            this.DownloadQueueDesignComponent.LowRight = true;
            this.DownloadQueueDesignComponent.UpLeft = true;
            this.DownloadQueueDesignComponent.UpRight = true;
            this.DownloadQueueDesignComponent.XRadius = 7F;
            this.DownloadQueueDesignComponent.YRadius = 8F;
            // 
            // ClearButtonDesignComponent
            // 
            this.ClearButtonDesignComponent._SetButton = this.ClearButton;
            this.ClearButtonDesignComponent.BackGroundHtmlColorCode = "#17A589";
            this.ClearButtonDesignComponent.ForeGroundHtmlColorCode = "#1C2833";
            this.ClearButtonDesignComponent.LowLeft = true;
            this.ClearButtonDesignComponent.LowRight = true;
            this.ClearButtonDesignComponent.UpLeft = true;
            this.ClearButtonDesignComponent.UpRight = true;
            this.ClearButtonDesignComponent.XRadius = 7F;
            this.ClearButtonDesignComponent.YRadius = 6F;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 476);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.ProcessDataGridView);
            this.Controls.Add(this.DownloadQueueButton);
            this.Controls.Add(this.AddToQueueButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.UrlTextBox);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adobe Connect Downloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DataGridContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProcessDataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ToolStripMenuItem convertFlvvideosToMp4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeZipFileToolStripMenuItem;
        private DesignComponent DownloadQueueDesignComponent;
        private System.Windows.Forms.ToolStripMenuItem convertYourZipFileToVideoToolStripMenuItem;
        private DesignComponent ClearButtonDesignComponent;
        private System.Windows.Forms.ToolStripMenuItem convertFlvAudioToMP3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadPdfToolStripMenuItem;
    }
}
