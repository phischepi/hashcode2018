using System.Collections.Generic;
using System.IO;
using System.Text;
using HashCode.Model;

namespace HashCode
{
    internal class WriterHelper
    {
        private const string FileName = "result.out";

        public static void WriteResult<TVehicule>(IEnumerable<TVehicule> vehicules, bool overwrite = false)
            where TVehicule : Vehicule
        {
            var builder = new StringBuilder();
            foreach (var vehicule in vehicules) builder.AppendLine(vehicule.ToString());
            File.WriteAllText($".{Path.DirectorySeparatorChar}Out{Path.DirectorySeparatorChar}{FileName}",
                builder.ToString());
        }
    }
}