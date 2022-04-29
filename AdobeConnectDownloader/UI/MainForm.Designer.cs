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
            this.AddToQueeButton = new System.Windows.Forms.Button();
            this.DownloadQueeButton = new System.Windows.Forms.Button();
            this.FileFolderAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UrlColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessDataGridView = new System.Windows.Forms.DataGridView();
            this.AddToQueeButtonDesignComponent = new AdobeConnectDownloader.UI.DesignComponent(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.convertFlvToMp4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertFlvvideosToMp4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertYourZipFileToVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeZipFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadQueeDesignComponent = new AdobeConnectDownloader.UI.DesignComponent(this.components);
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
            // 
            // ClearButton
            // 
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
            // AddToQueeButton
            // 
            this.AddToQueeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(47)))), ((int)(((byte)(60)))));
            this.AddToQueeButton.FlatAppearance.BorderSize = 0;
            this.AddToQueeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddToQueeButton.ForeColor = System.Drawing.Color.White;
            this.AddToQueeButton.Location = new System.Drawing.Point(143, 64);
            this.AddToQueeButton.Name = "AddToQueeButton";
            this.AddToQueeButton.Size = new System.Drawing.Size(108, 36);
            this.AddToQueeButton.TabIndex = 10;
            this.AddToQueeButton.Text = "Add To Quee";
            this.AddToQueeButton.UseVisualStyleBackColor = false;
            this.AddToQueeButton.Click += new System.EventHandler(this.AddToQueeButton_Click);
            // 
            // DownloadQueeButton
            // 
            this.DownloadQueeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(47)))), ((int)(((byte)(60)))));
            this.DownloadQueeButton.FlatAppearance.BorderSize = 0;
            this.DownloadQueeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadQueeButton.ForeColor = System.Drawing.Color.White;
            this.DownloadQueeButton.Location = new System.Drawing.Point(12, 64);
            this.DownloadQueeButton.Name = "DownloadQueeButton";
            this.DownloadQueeButton.Size = new System.Drawing.Size(113, 36);
            this.DownloadQueeButton.TabIndex = 12;
            this.DownloadQueeButton.Text = "Download Quee";
            this.DownloadQueeButton.UseVisualStyleBackColor = false;
            this.DownloadQueeButton.Click += new System.EventHandler(this.DownloadQueeButton_Click);
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
            // AddToQueeButtonDesignComponent
            // 
            this.AddToQueeButtonDesignComponent._SetButton = this.AddToQueeButton;
            this.AddToQueeButtonDesignComponent.BackGroundHtmlColorCode = "#212F3C";
            this.AddToQueeButtonDesignComponent.ForeGroundHtmlColorCode = "White";
            this.AddToQueeButtonDesignComponent.LowLeft = true;
            this.AddToQueeButtonDesignComponent.LowRight = true;
            this.AddToQueeButtonDesignComponent.UpLeft = true;
            this.AddToQueeButtonDesignComponent.UpRight = true;
            this.AddToQueeButtonDesignComponent.XRadius = 6F;
            this.AddToQueeButtonDesignComponent.YRadius = 8F;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertFlvToMp4ToolStripMenuItem,
            this.zipToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(696, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // convertFlvToMp4ToolStripMenuItem
            // 
            this.convertFlvToMp4ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertFlvvideosToMp4ToolStripMenuItem,
            this.convertYourZipFileToVideoToolStripMenuItem});
            this.convertFlvToMp4ToolStripMenuItem.Name = "convertFlvToMp4ToolStripMenuItem";
            this.convertFlvToMp4ToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.convertFlvToMp4ToolStripMenuItem.Text = "Convert";
            this.convertFlvToMp4ToolStripMenuItem.Click += new System.EventHandler(this.convertFlvToMp4ToolStripMenuItem_Click);
            // 
            // convertFlvvideosToMp4ToolStripMenuItem
            // 
            this.convertFlvvideosToMp4ToolStripMenuItem.Name = "convertFlvvideosToMp4ToolStripMenuItem";
            this.convertFlvvideosToMp4ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.convertFlvvideosToMp4ToolStripMenuItem.Text = "Convert Flv (videos) to mp4";
            // 
            // convertYourZipFileToVideoToolStripMenuItem
            // 
            this.convertYourZipFileToVideoToolStripMenuItem.Name = "convertYourZipFileToVideoToolStripMenuItem";
            this.convertYourZipFileToVideoToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.convertYourZipFileToVideoToolStripMenuItem.Text = "Convert your zip file to video";
            this.convertYourZipFileToVideoToolStripMenuItem.Click += new System.EventHandler(this.convertYourZipFileToVideoToolStripMenuItem_Click);
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
            this.mergeZipFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mergeZipFileToolStripMenuItem.Text = "About Creator";

            // 
            // DownloadQueeDesignComponent
            // 
            this.DownloadQueeDesignComponent._SetButton = this.DownloadQueeButton;
            this.DownloadQueeDesignComponent.BackGroundHtmlColorCode = "#212F3C";
            this.DownloadQueeDesignComponent.ForeGroundHtmlColorCode = "White";
            this.DownloadQueeDesignComponent.LowLeft = true;
            this.DownloadQueeDesignComponent.LowRight = true;
            this.DownloadQueeDesignComponent.UpLeft = true;
            this.DownloadQueeDesignComponent.UpRight = true;
            this.DownloadQueeDesignComponent.XRadius = 7F;
            this.DownloadQueeDesignComponent.YRadius = 8F;
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
            this.Controls.Add(this.DownloadQueeButton);
            this.Controls.Add(this.AddToQueeButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.UrlTextBox);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adobe Connect Downloader";
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
        private System.Windows.Forms.Button AddToQueeButton;
        private System.Windows.Forms.ContextMenuStrip DataGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editSaveFolderAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Button DownloadQueeButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFolderAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UrlColumn;
        private System.Windows.Forms.DataGridView ProcessDataGridView;
        private DesignComponent AddToQueeButtonDesignComponent;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem convertFlvToMp4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertFlvvideosToMp4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeZipFileToolStripMenuItem;
        private DesignComponent DownloadQueeDesignComponent;
        private System.Windows.Forms.ToolStripMenuItem convertYourZipFileToVideoToolStripMenuItem;
        private DesignComponent ClearButtonDesignComponent;
    }
}
