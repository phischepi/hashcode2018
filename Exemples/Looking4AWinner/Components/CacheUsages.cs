using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;

namespace Looking4AWinner.LikeUs.Components
{
    public class CacheUsages : IGameOutput
    {
        public ICollection<DataCache> Caches = new HashSet<DataCache>();

        /// <summary>
        ///     Exports the specified target file path.
        /// </summary>
        /// <param name="targetFilePath">The target file path.</param>
        public void Export(string targetFilePath)
        {
            var caches = Caches.Where(c => c.Videos.Any() && c.Id > 0).ToList();
            File.WriteAllLines(targetFilePath, new[]
            {
                caches.Count.ToString()
            }.Concat(caches.OrderBy(c => c.Id).Select(s => s.ToString())));
        }

        /// <summary>
        ///     Gets the result.
        /// </summary>
        /// <returns></returns>
        public int GetResult()
        {
            //return
            //    Caches.SelectMany(c => c.Videos, (cache, video) => new {cache, video})
            //        .Sum(
            //            v =>
            //                v.video.Requests.Sum(
            //                    r =>
            //                        r.RequestCount *
            //                        (r.EndPoint.CenterLatency.Time - (r.EndPoint.GetCacheLatencyTime(v.cache) ??
            //                                                          r.EndPoint.CenterLatency.Time)))) /
            //    _game.Requests.Sum(r => r.RequestCount) * 1000;
            return 0;
        }
    }
}