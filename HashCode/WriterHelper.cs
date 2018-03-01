using System.Diagnostics;
using System.IO;
using HashCode.Model;

namespace HashCode
{
    internal class WriterHelper
    {

        public static void WriteResult(Simulation sim, bool overwrite = false)
        {
            var dir = new DirectoryInfo($".{Path.DirectorySeparatorChar}Out");
            if (!dir.Exists)
                Directory.CreateDirectory(dir.FullName);
            var info = new FileInfo($"{dir.FullName}{Path.DirectorySeparatorChar}{sim.InputFile}");
            if (info.Exists)
                File.Delete(info.FullName);
            using (var outputFile = new StreamWriter(info.FullName))
            {
                foreach (var vehicule in sim.Vehicules)
                    outputFile.WriteLine(vehicule.ToString());
            }

            Process.Start(dir.FullName);
        }
    }
}