using Downloader_YouTube_Videos.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Downloader_YouTube_Videos
{
    public partial class frmLoadScreen : Form
    {
        private string _YtDlpPath = "";
        private string _url = "";

        public string formats = "";
        public ProgressBarStyle Style
        {
            set
            {
                progressBar1.Style = value;

                if (value == ProgressBarStyle.Continuous)
                {
                    
                    lblPercent.Visible = true;
                    lblPleaseWait.Text = "Downloading ...";
                    lblTitle.Visible = true;

                    picbLoad.Image = Resources.DownloadIcon;
                    picbLoad.SizeMode = PictureBoxSizeMode.StretchImage;
                    picbLoad.Size = new Size(164, 92);
                    picbLoad.Location = new Point(478, -8);

                }
                else
                {
                    lblPleaseWait.Text = "Please wait ...";
                    lblPercent.Visible = false;
                    
                    lblTitle.Visible = false;

                   
                    picbLoad.Image = Resources.loading12;
                    picbLoad.SizeMode = PictureBoxSizeMode.Zoom;
                    picbLoad.Size = new Size(106, 83);
                    picbLoad.Location = new Point(242, 1);
                }

            }
        }


        public frmLoadScreen(ProgressBarStyle Style , string Title)
        {
            InitializeComponent();
            this.Style = Style;
            lblTitle.Text = Title;//Mind Blowing OLED Dolby Vision - 4K HDR 240 fps
        }

        public frmLoadScreen(ProgressBarStyle Style , string YtDlpPath , string url)
        {
            InitializeComponent();

            this.Style = Style;
            _YtDlpPath = YtDlpPath;
            _url = url;
        }

        public void UpdateProgress(int percent)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgress), percent);
                return;
            }

            percent = Math.Max(0, Math.Min(100, percent));

            progressBar1.Value = percent;

            lblPercent.Text = percent + "%";

            //if (progressBar1.Value >= 100)
            //{
            //    this.Close();
            //}


        }


        public async Task<string> GetFormatsAsync(string url) //butSearch_Click
        {
            StringBuilder outputBuilder = new StringBuilder();

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = _YtDlpPath,
                Arguments = $"-F \"{url}\" --no-warnings --quiet --no-playlist",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = psi };

            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                    outputBuilder.AppendLine(e.Data);
            };

            var tcs = new TaskCompletionSource<string>();

            process.EnableRaisingEvents = true;
            process.Exited += (s, e) =>
            {
                tcs.SetResult(outputBuilder.ToString());
                process.Dispose();
            };

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return await tcs.Task;
        }

        private async void frmLoadScreen_Load(object sender, EventArgs e)
        {
            if (progressBar1.Style == ProgressBarStyle.Marquee)
            {
                this.formats = await GetFormatsAsync(_url);

                this.DialogResult = DialogResult.OK;
                this.Close();

            }

            

        }
    }
}
