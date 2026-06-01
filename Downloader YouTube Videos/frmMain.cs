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

        private async Task<string> GetFormatsAsync(string url) //butSearch_Click
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

        private void LoadToDataTable(string input)
        {
            string[] lines = input.Split('\n');

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // تجاهل الهيدر
                if (line.StartsWith("ID"))
                    continue;

                // تقسيم باستخدام مسافات متعددة
                var parts = System.Text.RegularExpressions.Regex
                    .Split(line.Trim(), @"\s+");

                if (parts.Length < 4)
                    continue;

                string id = parts[0];
                string ext = parts[1];
                string resolution = parts[2];
                string fps = parts.Length > 3 ? parts[3] : "";
                string size = parts.Length > 5 ? parts[5] : "";

                _dtFormats.Rows.Add(id, ext, resolution, fps, size, line);
            }

        }

        async Task<string> GetTitleVideoAsync()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _YtDlpPath,
                    Arguments = $"--get-title \"{txtLinkURL.Text}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8 // لضمان دعم اللغة العربية
                };

                using (var process = new Process { StartInfo = psi })
                {
                    process.Start();

                    // قراءة النتيجة من الـ StandardOutput
                    string title = await process.StandardOutput.ReadToEndAsync();
                    await Task.Run(() => process.WaitForExit());

                    return title.Trim();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
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


        async Task<bool> DownloadAsync2()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _YtDlpPath,
                    Arguments = $"-f {_FormatID} \"{txtLinkURL.Text}\" -o \"{_DownloadPath}\\%(title)s.%(ext)s\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                var process = new Process { StartInfo = psi };

                process.Start();

                while (!process.StandardOutput.EndOfStream)
                {
                    string line = await process.StandardOutput.ReadLineAsync();

                    // البحث عن النسبة المئوية في السطر (مثل 45.2%)
                    var match = System.Text.RegularExpressions.Regex.Match(line, @"(\d+(\.\d+)?)(?=%)");
                    if (match.Success)
                    {
                        if (float.TryParse(match.Value, out float percent))
                        {
                            // تمرير الرقم إلى الدالة
                            loadForm.UpdateProgress((int)percent);
                        }
                    }
                }

                string error = await process.StandardError.ReadToEndAsync();

                await Task.Run(() => process.WaitForExit());

                if (process.ExitCode == 0)
                    return true;

                MessageBox.Show(error, "Error");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        async Task<bool> DownloadAsync()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _YtDlpPath,
                    Arguments = $"--newline -f {_FormatID} \"{txtLinkURL.Text}\" -o \"{_DownloadPath}\\%(title)s.%(ext)s\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                var process = new Process();
                process.StartInfo = psi;

                process.OutputDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        var match = Regex.Match(e.Data, @"(\d+(\.\d+)?)(?=%)");

                        if (match.Success)
                        {
                            if (float.TryParse(match.Value, out float percent))
                            {
                                loadForm.UpdateProgress((int)percent);
                            }
                        }
                    }
                };

                process.Start();

                process.BeginOutputReadLine();

                await Task.Run(() => process.WaitForExit());

                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        bool UpdateYtDlp()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = _YtDlpPath,
                    Arguments = "-U",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = Process.Start(psi);
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                MessageBox.Show(output, "Update...");
                return true;
            }
            catch
            {
                return false;
            }
        }

        string GetLocalVersion()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = _YtDlpPath,
                Arguments = "--version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(psi);
            string output = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            return output;
        }

        string GetLatestVersion()
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("User-Agent", "request");
                string json = wc.DownloadString("https://api.github.com/repos/yt-dlp/yt-dlp/releases/latest");

                // استخراج tag_name
                var match = System.Text.RegularExpressions.Regex.Match(json, "\"tag_name\":\"(.*?)\"");
                return match.Groups[1].Value.Replace("yt-dlp ", "").Trim();
            }
        }

        bool IsUpdateAvailable()
        {
            string local = GetLocalVersion();
            string latest = GetLatestVersion();

            return local != latest;
        }

        private async Task LoadTitleAsync()
        {
            lblTitle.Text = await GetTitleVideoAsync();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            txtLinkURL.Enabled = false;
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
                    LoadToDataTable(formats);
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            //if (!IsUpdateAvailable())
            //{
            //    lblIsReady.Text = "Ready";
            //    lblIsReady.ForeColor = Color.Green;

            //    butUpdate.Visible = false;
            //}
            //else
            //{
            //    lblIsReady.Text = "Not Ready";
            //    lblIsReady.ForeColor = Color.Red;
            //    butUpdate.Visible = true;

            //}

            _dtFormats.Columns.Add("ID");
            _dtFormats.Columns.Add("EXT");
            _dtFormats.Columns.Add("Resolution");
            _dtFormats.Columns.Add("FPS");
            _dtFormats.Columns.Add("Size");
            _dtFormats.Columns.Add("Info");

        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            UpdateYtDlp();
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

            string filePath = Path.Combine(_DownloadPath, lblTitle.Text + $".{cbExt.Text}");

            if (File.Exists(filePath))// is already exist
            {
                MessageBox.Show($"The video already exists in \n {_DownloadPath}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            string filePath = Path.Combine(_DownloadPath, lblTitle.Text + $".{cbExt.Text}");

            if (File.Exists(filePath))// is already exist
            {
                MessageBox.Show($"The video already exists in \n {_DownloadPath}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


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
            loadForm = new frmLoadScreen(ProgressBarStyle.Continuous, lblTitle.Text);

            loadForm.Show();

            bool DownloadTask = await DownloadAsync();

            loadForm.Close();

            if (DownloadTask)
            {
                var rc = MessageBox.Show($"Download Successful.\n\nPath:\"{_DownloadPath}\"\n\nDo you want to open the download Path?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (rc == DialogResult.Yes)
                {
                    Process.Start("explorer.exe", $"/select,\"{filePath}\"");

                }

            }
            else
                MessageBox.Show("Download Failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        


    }
}
