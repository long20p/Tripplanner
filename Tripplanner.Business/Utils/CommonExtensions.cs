using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Tripplanner.Business.Utils
{
    public static class CommonExtensions
    {
        private static readonly string[] TextFileExtensions = {".json", ".xml", ".txt"};

        public static bool IsText(this ZipArchiveEntry entry)
        {
            return TextFileExtensions.Any(ext => entry.Name.EndsWith(ext));
        }
    }
}
