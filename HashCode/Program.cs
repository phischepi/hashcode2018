using HashCode.Model;

namespace HashCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var simulation = new Simulation();
            WriterHelper.Init(simulation);

            for (var i = 0; i < simulation.Steps; i++)
                simulation.ComputeNextStep();
        }
    }
}