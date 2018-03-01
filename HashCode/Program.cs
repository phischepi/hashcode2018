using HashCode.Algo;
using HashCode.Model;

namespace HashCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var simulation = new Simulation();
            var sims = ReaderHelper.Init();

            foreach (var sim in sims)
            {
                var algo = new JSY();
                algo.Execute(simulation);

                WriterHelper.WriteResult(simulation);
            }
        }
    }
}