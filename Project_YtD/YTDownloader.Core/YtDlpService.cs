using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YTDownloader.Core
{
    public class clsYtDlpService
    {
        private readonly string _YtDlpPath;

        public clsYtDlpService(string ytDlpPath)
        {
            _YtDlpPath = ytDlpPath;
        }

        public async Task<string> GetFormatsAsync(string URL)//butSearch_Click
        {
            StringBuilder outputBuilder = new StringBuilder();

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = _YtDlpPath,
                Arguments = $"-F \"{URL}\" --no-warnings --quiet --no-playlist",
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

        public async Task<string> GetVideoTitleAsync(string URL)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _YtDlpPath,
                    Arguments = $"--get-title \"{URL}\"",
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

        public async Task<bool> DownloadAsync(string URL,string FormatID,string DownloadPath,Action<int,string> ProgressCallback) 
        {
            try
            {
                
                var psi = new ProcessStartInfo
                {
                    FileName = _YtDlpPath,
                    //Arguments = $"--newline -f {FormatID} \"{URL}\" -o \"{DownloadPath}\\%(title)s.%(ext)s\"",
                    Arguments = $"--newline -f {FormatID} \"{URL}\" -o \"{DownloadPath}\"",
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
                        //percent
                        var match = Regex.Match(e.Data, @"(\d+(\.\d+)?)(?=%)");

                        //size and speed
                        var match2 = Regex.Match(e.Data, @"\d+(\.\d+)?(?:KiB|MiB|GiB)\s+at\s+\d+(\.\d+)?(?:KiB|MiB|GiB)/s"); 

                        if (match.Success && match2.Success)
                        {
                            if (float.TryParse(match.Value, out float percent) )
                            {
                                ProgressCallback?.Invoke((int)percent, match2.Value);
                            }
                        }


                    }
                };

                process.Start();

                process.BeginOutputReadLine();

                string error = await process.StandardError.ReadToEndAsync();

                await Task.Run(() => process.WaitForExit());

                return process.ExitCode == 0;

                
                
            }
            catch// (Exception ex)
            {
                
                return false;
            }
        }

        public async Task<bool> DownloadAsync2(string URL, string FormatID, string DownloadPath, Action<int> ProgressCallback)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _YtDlpPath,
                    Arguments = $"-f {FormatID} \"{URL}\" -o \"{DownloadPath}\\%(title)s.%(ext)s\"",
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
                            ProgressCallback?.Invoke((int)percent);
                        }
                    }
                }

                string error = await process.StandardError.ReadToEndAsync();

                await Task.Run(() => process.WaitForExit());

                if (process.ExitCode == 0)
                    return true;

                
                return false;
            }
            catch //(Exception ex)
            {
                
                return false;
            }
        }

        public bool Update()
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

                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetLocalVersion()
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

        public string GetLatestVersion()
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

        public bool IsUpdateAvailable()
        {
            string local = GetLocalVersion();
            string latest = GetLatestVersion();

            return local != latest;
        }



    }
}
