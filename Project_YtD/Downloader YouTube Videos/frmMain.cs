using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTDownloader.Core;



namespace Downloader_YouTube_Videos
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private DataTable _dtFormats = new DataTable();
        frmLoadScreen loadForm;


        private string _YtDlpPath = Path.Combine(Application.StartupPath, "yt-dlp.exe");
        private string _DownloadPath = "";
        private string _FormatID = "";
        private string _URL = "";


        private clsYtDlpService YtDlpService;

        private void frmMain_Load(object sender, EventArgs e)
        {
            _dtFormats.Columns.Add("ID");
            _dtFormats.Columns.Add("EXT");
            _dtFormats.Columns.Add("Resolution");
            _dtFormats.Columns.Add("FPS");
            _dtFormats.Columns.Add("Size");
            _dtFormats.Columns.Add("Info");

            YtDlpService = new clsYtDlpService(_YtDlpPath);

        }

        private void _FillResolutionInComboBox()
        {
            cbResolution.Items.Clear();

            foreach (DataRow row in _dtFormats.Rows)
            {
                string resolution = row["resolution"].ToString();

                if (!cbResolution.Items.Contains(resolution))
                    cbResolution.Items.Add(resolution);
            }

        }

        private void _FillExtInComboBox()
        {
            cbExt.Items.Clear();

            foreach (DataRow row in _dtFormats.Rows)
            {
                string ext = row["ext"].ToString();

                if (!cbExt.Items.Contains(ext))
                {
                    cbExt.Items.Add(ext);
                }

            }

            cbExt.SelectedIndex = cbExt.FindStringExact("mp4");

        }

        private void cbExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbResolution.Items.Clear();

            foreach (DataRow row in _dtFormats.Rows)
            {
                string resolution = row["resolution"].ToString();

                if (cbExt.Text == row["ext"].ToString())
                {
                    if (!cbResolution.Items.Contains(resolution))
                        cbResolution.Items.Add(resolution);
                }

            }

            if (!(cbResolution.SelectedIndex != -1))
            {
                lblSize.Text = "ID : ???? | Size: ????";
            }

            butDownload.Enabled = (cbResolution.SelectedIndex != -1) && (cbExt.SelectedIndex != -1);



            ApplyFilterOfComboBox();

        }

        private void ApplyFilterOfComboBox()
        {
            string FilterColumn = cbExt.Text;

            _dtFormats.DefaultView.RowFilter = string.Format($"[EXT] = '{FilterColumn}'"); //[FilterColumn] = txtFilterBy.Text

        }

        private void cbResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;

            foreach (DataGridViewRow row in dgvFormats.Rows)
            {
                if (cbResolution.Text == row.Cells["resolution"].Value.ToString())
                {
                    string ID = row.Cells["id"].Value.ToString();

                    if (cbExt.Text == row.Cells["ext"].Value.ToString())
                    { 
                        lblSize.Text = $"ID : {ID} | Size: {row.Cells["size"].Value}";//ID : ???? | Size: ????

                        //dgvFormats.ClearSelection();

                        dgvFormats.Rows[i].Selected = true;
                        dgvFormats.CurrentCell = dgvFormats.Rows[i].Cells[0];
                    }


                    if (cbExt.Text == "mp4")
                    {
                        if (cbResolution.Text != "audio")
                        {
                            if (chbWithEditorVideo.Checked)
                                _FormatID = $"\"{ID}+bestaudio[ext=m4a]\" --recode-video mp4";
                            else
                                _FormatID = $"\"{ID}+bestaudio[ext=m4a]\" --merge-output-format mp4";
                        }

                    }
                    else if (cbExt.Text == "webm")
                    {
                        if (cbResolution.Text != "audio")
                        {
                            _FormatID = $"\"{ID}+bestaudio[ext=webm]\" --merge-output-format webm";
                        }

                    }
                    else
                        _FormatID = $"{ID}";

                    break;
                }

                i++;
            }

            butDownload.Enabled = (cbResolution.SelectedIndex != -1) && (cbExt.SelectedIndex != -1);


        }

        private async Task LoadTitleAsync()
        {
            lblTitle.Text = await YtDlpService.GetVideoTitleAsync(_URL);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            txtLinkURL.Enabled = false;
            _URL = txtLinkURL.Text;
            lblTitle.Text = "Loading...";

            loadForm = new frmLoadScreen(ProgressBarStyle.Marquee, _YtDlpPath, txtLinkURL.Text);

            try
            {
                _ = LoadTitleAsync(); // This tells the compiler I deliberately don't use await [ _ = ]

                loadForm.ShowDialog();

                //string formats = await GetFormatsAsync(txtLinkURL.Text);

                string formats = loadForm.formats;

                if (!string.IsNullOrEmpty(formats))
                {
                    _dtFormats.Rows.Clear();
                    clsFormatParser.LoadFormats(_dtFormats, formats);
                    dgvFormats.DataSource = _dtFormats;
                    _FormatDGV();
                    

                    _FillResolutionInComboBox();
                    _FillExtInComboBox();
                }
                else
                {
                    MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _FormatDGV()
        {
            if (dgvFormats.Rows.Count <= 0)
            {
                return;
            }

            dgvFormats.Columns[0].Width = 57;
            dgvFormats.Columns[1].Width = 57;
            dgvFormats.Columns[2].Width = 105;
            dgvFormats.Columns[3].Width = 50;
            dgvFormats.Columns[4].Width = 91;
            dgvFormats.Columns[5].Width = 770;


        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            YtDlpService.Update();
        }

        private void butPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();

                if (text.StartsWith("http"))
                {
                    txtLinkURL.Text = text;
                }
                else
                {
                    MessageBox.Show("The text is not a valid link.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void butDownload_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _DownloadPath = folderBrowserDialog1.SelectedPath;
            }

            if (string.IsNullOrEmpty(_DownloadPath))
                return;

            string safeTitle = clsFileHelper.SanitizeFileName(lblTitle.Text);

            string filePath = Path.Combine(_DownloadPath,$"{safeTitle}.{cbExt.Text}");

            //if (File.Exists(filePath))// is already exist
            //{
            //    MessageBox.Show($"The video already exists in \n {_DownloadPath}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            filePath = clsFileHelper.GetUniqueFilePath(filePath);

            _ = HandleDownloadAsync(filePath);

        }

        private void tsmDownload_Click(object sender, EventArgs e)
        {
            if (dgvFormats.Rows.Count <= 0)
                return;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _DownloadPath = folderBrowserDialog1.SelectedPath;
            }

            if (string.IsNullOrEmpty(_DownloadPath))
                return;

            string safeTitle = clsFileHelper.SanitizeFileName(lblTitle.Text);

            string filePath = Path.Combine(_DownloadPath, $"{safeTitle}.{cbExt.Text}");

            //if (File.Exists(filePath))// is already exist
            //{
            //    MessageBox.Show($"The video already exists in \n {_DownloadPath}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            filePath = clsFileHelper.GetUniqueFilePath(filePath);


            string ID = (string)dgvFormats.CurrentRow.Cells[0].Value;
            string EXT = (string)dgvFormats.CurrentRow.Cells[1].Value;
            string Resolution = (string)dgvFormats.CurrentRow.Cells[2].Value;

            if (Resolution == "audio")
            {
                _FormatID = $"{ID}";
            }
            else if (EXT == "mp4")
            {
                if (chbWithEditorVideo.Checked)
                    _FormatID = $"\"{ID}+bestaudio[ext=m4a]\" --recode-video mp4";
                else
                    _FormatID = $"\"{ID}+bestaudio[ext=m4a]\" --merge-output-format mp4";
            }
            else if (EXT == "webm")
            {
                _FormatID = $"\"{ID}+bestaudio[ext=webm]\" --merge-output-format webm";
            }
            else
                _FormatID = $"{ID}";


            _ = HandleDownloadAsync(filePath);

        }

        private async Task HandleDownloadAsync(string filePath)
        {
            loadForm = new frmLoadScreen(ProgressBarStyle.Continuous, clsFileHelper.SanitizeFileName(lblTitle.Text));

            loadForm.Show();

            bool DownloadTask = await YtDlpService.DownloadAsync(_URL,_FormatID,filePath, OnCallbackProgress);

            loadForm.Close();

            if (DownloadTask)
            {
                var rc = MessageBox.Show($"Download Successful.\n\nPath:\"{_DownloadPath}\"\n\nDo you want to open the download Path?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (rc == DialogResult.Yes)
                {
                    if (!File.Exists(filePath))
                        return;

                    Process.Start("explorer.exe", $"/select,\"{filePath}\"");
                    
                }

            }
            else
                MessageBox.Show("Download Failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }

        private void OnCallbackProgress(int progress, string info)
        {
            loadForm.UpdateProgress(progress, info);
        }

        
    }
}
