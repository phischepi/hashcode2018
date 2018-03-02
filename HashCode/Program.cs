using System.Threading;
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
                var newThread = new Thread(() =>
                {
                    var algo = new JSY();
                    algo.Execute(sim);
                    WriterHelper.WriteResult(sim);
                });
                newThread.Start();
            }
        }
    }
}