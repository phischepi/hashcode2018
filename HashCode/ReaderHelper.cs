using System;
using System.IO;
using System.Linq;
using HashCode.Model;

namespace HashCode
{
    internal class ReaderHelper
    {
        public static void Init(Simulation simulation, ConsoleKeyInfo inNr)
        {
            var dir = new DirectoryInfo($".{Path.DirectorySeparatorChar}In");
            var files = dir.EnumerateFiles().ToArray();


            var pos = (int) inNr.KeyChar;
            if (pos >= files.Length)
                return;

            Console.WriteLine($"Current input {files[pos].FullName}");
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