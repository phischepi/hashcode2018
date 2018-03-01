using System;
using System.IO;

namespace HashCode
{
    internal class WriterHelper
    {
        private const string FileName = "result.out";

        public bool WriteResult(bool overwrite = false)
        {
            var file = new FileInfo($".{Path.DirectorySeparatorChar}Output{Path.DirectorySeparatorChar}{FileName}");
            return false;
        }
    }
}