namespace Downloader_YouTube_Videos
{
    partial class frmLoadScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadScreen));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picbLoad = new System.Windows.Forms.PictureBox();
            this.lblSizeSpeed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picbLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 104);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(590, 34);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.Location = new System.Drawing.Point(7, 72);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(179, 29);
            this.lblPleaseWait.TabIndex = 1;
            this.lblPleaseWait.Text = "Downloading ...";
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercent.Location = new System.Drawing.Point(536, 72);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(48, 29);
            this.lblPercent.TabIndex = 3;
            this.lblPercent.Text = "0%";
            this.lblPercent.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(7, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(508, 54);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Title";
            // 
            // picbLoad
            // 
            this.picbLoad.Image = global::Downloader_YouTube_Videos.Properties.Resources.DownloadIcon;
            this.picbLoad.Location = new System.Drawing.Point(478, -8);
            this.picbLoad.Name = "picbLoad";
            this.picbLoad.Size = new System.Drawing.Size(164, 92);
            this.picbLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbLoad.TabIndex = 2;
            this.picbLoad.TabStop = false;
            // 
            // lblSizeSpeed
            // 
            this.lblSizeSpeed.AutoSize = true;
            this.lblSizeSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSizeSpeed.Location = new System.Drawing.Point(235, 79);
            this.lblSizeSpeed.Name = "lblSizeSpeed";
            this.lblSizeSpeed.Size = new System.Drawing.Size(21, 20);
            this.lblSizeSpeed.TabIndex = 5;
            this.lblSizeSpeed.Text = "...";
            this.lblSizeSpeed.Visible = false;
            // 
            // frmLoadScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 160);
            this.Controls.Add(this.lblSizeSpeed);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.picbLoad);
            this.Controls.Add(this.lblPleaseWait);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLoadScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load Screen";
            this.Load += new System.EventHandler(this.frmLoadScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbLoad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.PictureBox picbLoad;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSizeSpeed;
    }
}