namespace AdobeConnectDownloader
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
            this.LinkProcessorButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.ZipProcessButton = new System.Windows.Forms.Button();
            this.ProcessDataGridView = new System.Windows.Forms.DataGridView();
            this.UrlColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileFolderAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editSaveFolderAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToQueeButton = new System.Windows.Forms.Button();
            this.DownloadQueeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessDataGridView)).BeginInit();
            this.DataGridContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // UrlTextBox
            // 
            this.UrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UrlTextBox.Location = new System.Drawing.Point(12, 16);
            this.UrlTextBox.Name = "UrlTextBox";
            this.UrlTextBox.PlaceholderText = "Url : ";
            this.UrlTextBox.Size = new System.Drawing.Size(661, 23);
            this.UrlTextBox.TabIndex = 3;
            // 
            // LinkProcessorButton
            // 
            this.LinkProcessorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.LinkProcessorButton.FlatAppearance.BorderSize = 0;
            this.LinkProcessorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LinkProcessorButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LinkProcessorButton.Location = new System.Drawing.Point(192, 45);
            this.LinkProcessorButton.Name = "LinkProcessorButton";
            this.LinkProcessorButton.Size = new System.Drawing.Size(113, 29);
            this.LinkProcessorButton.TabIndex = 4;
            this.LinkProcessorButton.Text = "Download Url";
            this.LinkProcessorButton.UseVisualStyleBackColor = false;
            this.LinkProcessorButton.Click += new System.EventHandler(this.LinkProcessorButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.Color.Green;
            this.ClearButton.FlatAppearance.BorderSize = 0;
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ClearButton.Location = new System.Drawing.Point(121, 45);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(65, 29);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // ZipProcessButton
            // 
            this.ZipProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZipProcessButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ZipProcessButton.FlatAppearance.BorderSize = 0;
            this.ZipProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ZipProcessButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ZipProcessButton.Location = new System.Drawing.Point(597, 45);
            this.ZipProcessButton.Name = "ZipProcessButton";
            this.ZipProcessButton.Size = new System.Drawing.Size(76, 29);
            this.ZipProcessButton.TabIndex = 8;
            this.ZipProcessButton.Text = "Zip Process";
            this.ZipProcessButton.UseVisualStyleBackColor = false;
            this.ZipProcessButton.Click += new System.EventHandler(this.ZipProcessButton_Click);
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
            this.ProcessDataGridView.Location = new System.Drawing.Point(12, 80);
            this.ProcessDataGridView.Name = "ProcessDataGridView";
            this.ProcessDataGridView.ReadOnly = true;
            this.ProcessDataGridView.RowTemplate.Height = 25;
            this.ProcessDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProcessDataGridView.Size = new System.Drawing.Size(661, 353);
            this.ProcessDataGridView.TabIndex = 9;
            // 
            // UrlColumn
            // 
            this.UrlColumn.HeaderText = "Url";
            this.UrlColumn.Name = "UrlColumn";
            this.UrlColumn.ReadOnly = true;
            // 
            // FileFolderAddressColumn
            // 
            this.FileFolderAddressColumn.HeaderText = "Save Folder";
            this.FileFolderAddressColumn.Name = "FileFolderAddressColumn";
            this.FileFolderAddressColumn.ReadOnly = true;
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
            this.AddToQueeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.AddToQueeButton.FlatAppearance.BorderSize = 0;
            this.AddToQueeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddToQueeButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.AddToQueeButton.Location = new System.Drawing.Point(12, 45);
            this.AddToQueeButton.Name = "AddToQueeButton";
            this.AddToQueeButton.Size = new System.Drawing.Size(103, 29);
            this.AddToQueeButton.TabIndex = 10;
            this.AddToQueeButton.Text = "Add To Quee";
            this.AddToQueeButton.UseVisualStyleBackColor = false;
            this.AddToQueeButton.Click += new System.EventHandler(this.AddToQueeButton_Click);
            // 
            // DownloadQueeButton
            // 
            this.DownloadQueeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.DownloadQueeButton.FlatAppearance.BorderSize = 0;
            this.DownloadQueeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadQueeButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.DownloadQueeButton.Location = new System.Drawing.Point(311, 45);
            this.DownloadQueeButton.Name = "DownloadQueeButton";
            this.DownloadQueeButton.Size = new System.Drawing.Size(113, 29);
            this.DownloadQueeButton.TabIndex = 12;
            this.DownloadQueeButton.Text = "Download Quee";
            this.DownloadQueeButton.UseVisualStyleBackColor = false;
            this.DownloadQueeButton.Click += new System.EventHandler(this.DownloadQueeButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 445);
            this.Controls.Add(this.DownloadQueeButton);
            this.Controls.Add(this.AddToQueeButton);
            this.Controls.Add(this.ProcessDataGridView);
            this.Controls.Add(this.ZipProcessButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.LinkProcessorButton);
            this.Controls.Add(this.UrlTextBox);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adobe Connect Downloader";
            ((System.ComponentModel.ISupportInitialize)(this.ProcessDataGridView)).EndInit();
            this.DataGridContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox UrlTextBox;
        private System.Windows.Forms.Button LinkProcessorButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button ZipProcessButton;
        private System.Windows.Forms.DataGridView ProcessDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn UrlColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFolderAddressColumn;
        private System.Windows.Forms.Button AddToQueeButton;
        private System.Windows.Forms.ContextMenuStrip DataGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editSaveFolderAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Button DownloadQueeButton;
    }
}
