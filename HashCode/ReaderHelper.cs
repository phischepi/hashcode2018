using System;
using System.IO;
using System.Linq;
using HashCode.Model;

namespace HashCode
{
    internal class ReaderHelper
    {
        public static void Init(Simulation simulation)
        {
            var dir = new DirectoryInfo($".{Path.DirectorySeparatorChar}In");
            var files = dir.EnumerateFiles().ToArray();

            for (var i = 0; i < files.Length; i++)
                Console.WriteLine($"[{i}] {files[i].Name}");

            var pos = int.Parse(Console.ReadKey().KeyChar.ToString());
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

            for (var i = 0; i < simulation.Rides.Count; i++)
            {
                var rideInfo = lines[i + 1].Split(' ');
                simulation.Rides[i].StartPoint = new Tuple<long, long>(long.Parse(rideInfo[0]), long.Parse(rideInfo[1]));
                simulation.Rides[i].EndPoint = new Tuple<long, long>(long.Parse(rideInfo[2]), long.Parse(rideInfo[3]));
                simulation.Rides[i].EarlyStart = long.Parse(rideInfo[4]);
                simulation.Rides[i].LatestFinish = long.Parse(rideInfo[5]);

            }

            foreach (var ride in lines.Skip(1))
            {
            }


            simulation.Bonus = long.Parse(meta[4]);
            simulation.Steps = long.Parse(meta[5]);
        }
    }
}