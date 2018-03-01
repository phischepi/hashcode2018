﻿using System.Collections.Generic;
using System.IO;
using HashCode.Model;

namespace HashCode
{
    internal class WriterHelper
    {
        private const string FileName = "result.out";

        public void WriteResult<TVehicule>(IEnumerable<TVehicule> vehicules, bool overwrite = false)
            where TVehicule : Vehicule
        {
            using (var file =
                new StreamWriter($".{Path.DirectorySeparatorChar}Output{Path.DirectorySeparatorChar}{FileName}"))
            {
                foreach (var vehicule in vehicules)
                    file.WriteLine(vehicule);
            }
        }
    }
}