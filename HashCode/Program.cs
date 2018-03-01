using System;
using HashCode.Algo;
using HashCode.Model;

namespace HashCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var inNr = Console.ReadKey();
            var simulation = new Simulation();
            ReaderHelper.Init(simulation, inNr);

            var algo = new JSY();
            algo.Execute(simulation);

            WriterHelper.WriteResult(simulation.Vehicules);
        }
    }
}