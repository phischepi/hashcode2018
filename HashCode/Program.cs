using HashCode.Algo;

namespace HashCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sims = ReaderHelper.Init();

            foreach (var sim in sims)
            {
                var algo = new JSY();
                algo.Execute(sim);

                WriterHelper.WriteResult(sim);
            }
        }
    }
}