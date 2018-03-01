using System;
using HashCode.Model;

namespace HashCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var inNr = Console.ReadKey();
            var simulation = new Simulation();
            WriterHelper.Init(simulation, inNr);

            for (var i = 0; i < simulation.Steps; i++)
                simulation.ComputeNextStep();
        }
    }
}