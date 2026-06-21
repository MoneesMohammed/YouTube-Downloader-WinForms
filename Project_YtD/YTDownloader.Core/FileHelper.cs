using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloader.Core
{
    public static class clsFileHelper
    {
        public static string SanitizeFileName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c.ToString(), "");
            }

            return fileName;
        }

        public static string GetUniqueFilePath(string filePath)
        {
            if (!File.Exists(filePath))
                return filePath;

            string directory = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            int counter = 1;

            while (true)
            {
                string newPath = Path.Combine(directory,$"{fileName} ({counter}){extension}");
                    
                if (!File.Exists(newPath) || File.Exists(newPath + ".part") || File.Exists(newPath + ".ytdl"))
                    return newPath;

                counter++;
            }
        }

    }

}
