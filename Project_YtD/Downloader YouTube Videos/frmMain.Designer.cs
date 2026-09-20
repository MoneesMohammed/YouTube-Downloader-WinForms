namespace Downloader_YouTube_Videos
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtLinkURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIsReady = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.dgvFormats = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cbExt = new System.Windows.Forms.ComboBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.butDownload = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.butPaste = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chbWithEditorVideo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormats)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLinkURL
            // 
            this.txtLinkURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLinkURL.Location = new System.Drawing.Point(12, 43);
            this.txtLinkURL.Name = "txtLinkURL";
            this.txtLinkURL.Size = new System.Drawing.Size(585, 35);
            this.txtLinkURL.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Link:";
            // 
            // lblIsReady
            // 
            this.lblIsReady.AutoSize = true;
            this.lblIsReady.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsReady.ForeColor = System.Drawing.Color.Green;
            this.lblIsReady.Location = new System.Drawing.Point(14, 122);
            this.lblIsReady.Name = "lblIsReady";
            this.lblIsReady.Size = new System.Drawing.Size(64, 24);
            this.lblIsReady.TabIndex = 5;
            this.lblIsReady.Text = "Ready";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(12, 84);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(125, 32);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.butUpdate_Click);
            // 
            // cbResolution
            // 
            this.cbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Items.AddRange(new object[] {
            "1080p",
            "720p",
            "Audio"});
            this.cbResolution.Location = new System.Drawing.Point(358, 148);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(178, 37);
            this.cbResolution.TabIndex = 7;
            this.cbResolution.SelectedIndexChanged += new System.EventHandler(this.cbResolution_SelectedIndexChanged);
            // 
            // dgvFormats
            // 
            this.dgvFormats.AllowUserToAddRows = false;
            this.dgvFormats.AllowUserToDeleteRows = false;
            this.dgvFormats.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFormats.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFormats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormats.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFormats.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFormats.Location = new System.Drawing.Point(15, 335);
            this.dgvFormats.Name = "dgvFormats";
            this.dgvFormats.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFormats.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFormats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFormats.Size = new System.Drawing.Size(707, 482);
            this.dgvFormats.TabIndex = 9;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDownload});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(187, 42);
            // 
            // tsmDownload
            // 
            this.tsmDownload.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmDownload.Image = global::Downloader_YouTube_Videos.Properties.Resources.download;
            this.tsmDownload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmDownload.Name = "tsmDownload";
            this.tsmDownload.Size = new System.Drawing.Size(186, 38);
            this.tsmDownload.Text = "Download";
            this.tsmDownload.Click += new System.EventHandler(this.tsmDownload_Click);
            // 
            // cbExt
            // 
            this.cbExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExt.FormattingEnabled = true;
            this.cbExt.Location = new System.Drawing.Point(174, 148);
            this.cbExt.Name = "cbExt";
            this.cbExt.Size = new System.Drawing.Size(178, 37);
            this.cbExt.TabIndex = 10;
            this.cbExt.SelectedIndexChanged += new System.EventHandler(this.cbExt_SelectedIndexChanged);
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(13, 303);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(249, 29);
            this.lblSize.TabIndex = 11;
            this.lblSize.Text = "ID : ???? | Size: ????";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(14, 206);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(708, 85);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(170, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "Extensions:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(354, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 24);
            this.label4.TabIndex = 16;
            this.label4.Text = "Resolution:";
            // 
            // butDownload
            // 
            this.butDownload.Enabled = false;
            this.butDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butDownload.Image = global::Downloader_YouTube_Videos.Properties.Resources.download;
            this.butDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butDownload.Location = new System.Drawing.Point(542, 146);
            this.butDownload.Name = "butDownload";
            this.butDownload.Size = new System.Drawing.Size(180, 39);
            this.butDownload.TabIndex = 8;
            this.butDownload.Text = "Download";
            this.butDownload.UseVisualStyleBackColor = true;
            this.butDownload.Click += new System.EventHandler(this.butDownload_Click);
            // 
            // butSearch
            // 
            this.butSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSearch.Image = global::Downloader_YouTube_Videos.Properties.Resources.search;
            this.butSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butSearch.Location = new System.Drawing.Point(603, 84);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(119, 42);
            this.butSearch.TabIndex = 3;
            this.butSearch.Text = "Search";
            this.butSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // butPaste
            // 
            this.butPaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butPaste.Image = global::Downloader_YouTube_Videos.Properties.Resources.paste;
            this.butPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butPaste.Location = new System.Drawing.Point(603, 36);
            this.butPaste.Name = "butPaste";
            this.butPaste.Size = new System.Drawing.Size(119, 42);
            this.butPaste.TabIndex = 2;
            this.butPaste.Text = "Paste";
            this.butPaste.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butPaste.UseVisualStyleBackColor = true;
            this.butPaste.Click += new System.EventHandler(this.butPaste_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(12, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 24);
            this.label3.TabIndex = 17;
            this.label3.Text = "Title:";
            // 
            // chbWithEditorVideo
            // 
            this.chbWithEditorVideo.AutoSize = true;
            this.chbWithEditorVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbWithEditorVideo.Location = new System.Drawing.Point(358, 84);
            this.chbWithEditorVideo.Name = "chbWithEditorVideo";
            this.chbWithEditorVideo.Size = new System.Drawing.Size(231, 24);
            this.chbWithEditorVideo.TabIndex = 18;
            this.chbWithEditorVideo.Text = "Compatible with Editor Video";
            this.chbWithEditorVideo.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 822);
            this.Controls.Add(this.chbWithEditorVideo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.cbExt);
            this.Controls.Add(this.dgvFormats);
            this.Controls.Add(this.butDownload);
            this.Controls.Add(this.cbResolution);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblIsReady);
            this.Controls.Add(this.butSearch);
            this.Controls.Add(this.butPaste);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLinkURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Downloader";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormats)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLinkURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butPaste;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Label lblIsReady;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cbResolution;
        private System.Windows.Forms.Button butDownload;
        private System.Windows.Forms.DataGridView dgvFormats;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox cbExt;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmDownload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbWithEditorVideo;
    }
}

