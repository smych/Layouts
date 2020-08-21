using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayotsMvvm.ViewModel.Base.ImageConverter
{
    public class FInfo
    {
        public FInfo(string _path)
        {
            path = _path;
            FileInfo _file = new FileInfo(path);
            GetExt = _file.Extension;
            GetSize = GetSizeString(_file.Length);
        }

        string path { get; set; }

        public string GetExt { get; set; }

        public string GetSize { get; set; }

        // public string GetWidth { get; set; }

        //public string GetHeight { get; set; }
        
        //FileInfo _file { get; set; }

        string GetSizeString(long length)
        {
            long B = 0, KB = 1024, MB = KB * 1024, GB = MB * 1024, TB = GB * 1024;
            double size = length;
            string suffix = nameof(B);

            if (length >= TB)
            {
                size = Math.Round((double)length / TB, 2);
                suffix = nameof(TB);
            }
            else if (length >= GB)
            {
                size = Math.Round((double)length / GB, 2);
                suffix = nameof(GB);
            }
            else if (length >= MB)
            {
                size = Math.Round((double)length / MB, 2);
                suffix = nameof(MB);
            }
            else if (length >= KB)
            {
                size = Math.Round((double)length / KB, 2);
                suffix = nameof(KB);
            }

            return $"{size} {suffix}";
        }

    }
}
