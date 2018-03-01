using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HashCode.Model;

namespace HashCode
{
    internal class ReaderHelper
    {
        public static IList<Simulation> Init()
        {
            var dir = new DirectoryInfo($".{Path.DirectorySeparatorChar}In");
            var files = dir.EnumerateFiles().ToArray();

            for (var i = 0; i < files.Length; i++)
                Console.WriteLine($"[{i}] {files[i].Name}");

            var input = Console.ReadKey().KeyChar.ToString();
            var one = int.TryParse(input, out var pos);
            if (pos >= files.Length)
                return new List<Simulation>();

            var filesToRead = !one ? files : files.Skip(pos).Take(1);

            var sim = new List<Simulation>();
            foreach (var file in filesToRead)
            {
                var simulation = new Simulation {InputFile = file.Name};
                Console.WriteLine($"Current input {file.FullName}");
                var lines = File.ReadAllLines(file.FullName);
                var meta = lines[0].Split(' ');
                simulation.Rows = long.Parse(meta[0]);
                simulation.Columns = long.Parse(meta[1]);
                for (long i = 0; i < long.Parse(meta[2]); i++)
                    simulation.Vehicules.Add(new Vehicule());

                for (long i = 0; i < long.Parse(meta[3]); i++)
                    simulation.Rides.Add(new Ride(i));

                for (var i = 0; i < simulation.Rides.Count; i++)
                {
                    var rideInfo = lines[i + 1].Split(' ');
                    simulation.Rides[i].StartPoint =
                        new Tuple<long, long>(long.Parse(rideInfo[0]), long.Parse(rideInfo[1]));
                    simulation.Rides[i].EndPoint =
                        new Tuple<long, long>(long.Parse(rideInfo[2]), long.Parse(rideInfo[3]));
                    simulation.Rides[i].EarlyStart = long.Parse(rideInfo[4]);
                    simulation.Rides[i].LatestFinish = long.Parse(rideInfo[5]);
                }

                simulation.Bonus = long.Parse(meta[4]);
                simulation.Steps = long.Parse(meta[5]);
                sim.Add(simulation);
            }

            return sim;
        }
    }
}