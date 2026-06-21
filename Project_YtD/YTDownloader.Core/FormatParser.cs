using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YTDownloader.Core
{
    public static class clsFormatParser
    {

        /// <summary>
        /// Parses the format list returned by yt-dlp and loads the extracted
        /// format information into the specified DataTable.
        /// </summary>
        /// <param name="dt">
        /// The DataTable that will be populated with format information.
        /// </param>
        /// <param name="input">
        /// Raw text output returned by yt-dlp containing available media formats.
        /// </param>
        public static void LoadFormats(DataTable dt, string input)
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
                var parts = Regex.Split(line.Trim(), @"\s+");

                if (parts.Length < 4)
                    continue;

                string id = parts[0];
                string ext = parts[1];
                string resolution = parts[2];
                string fps = parts.Length > 3 ? parts[3] : "";
                string size = parts.Length > 5 ? parts[5] : "";

                dt.Rows.Add(id, ext, resolution, fps, size, line);
            }
        }

        


    }
}
