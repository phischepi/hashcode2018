using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using HashCode.Model;

namespace HashCode
{
    internal class WriterHelper
    {
        private const string FileName = "result.out";

        public static void WriteResult<TVehicule>(IEnumerable<TVehicule> vehicules, bool overwrite = false)
            where TVehicule : Vehicule
        {
            var dir = new DirectoryInfo($".{Path.DirectorySeparatorChar}Out");
            if (!dir.Exists)
                Directory.CreateDirectory(dir.FullName);
            var info = new FileInfo($"{dir.FullName}{Path.DirectorySeparatorChar}{FileName}");
            if (info.Exists)
                File.Delete(info.FullName);
            using (var outputFile = new StreamWriter(info.FullName))
            {
                foreach (var vehicule in vehicules)
                    outputFile.WriteLine(vehicule.ToString());
            }

            Process.Start(dir.FullName);
        }
    }
}