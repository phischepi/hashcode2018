using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                new StreamWriter($".{Path.DirectorySeparatorChar}Out{Path.DirectorySeparatorChar}{FileName}"))
            {
                foreach (var vehicule in vehicules)
                    file.WriteLine(vehicule);
            }
        }

        public static void Init(Simulation simulation, ConsoleKeyInfo inNr)
        {
            var dir = new DirectoryInfo($".{Path.DirectorySeparatorChar}In");
            var files = dir.EnumerateFiles().ToArray();

            var pos = (int) inNr.KeyChar;
            if (pos >= files.Length)
                return;

            var lines = File.ReadAllLines(files[pos].FullName);
            var meta = lines[0].Split(' ');
            simulation.Rows = long.Parse(meta[0]);
            simulation.Columns = long.Parse(meta[1]);
            for (long i = 0; i < long.Parse(meta[2]); i++)
                simulation.Vehicules.Add(new Vehicule());

            for (long i = 0; i < long.Parse(meta[3]); i++)
                simulation.Rides.Add(new Ride());

            simulation.Bonus = long.Parse(meta[4]);
            simulation.Steps = long.Parse(meta[5]);
        }
    }
}