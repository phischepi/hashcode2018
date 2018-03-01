#region Imports

using System.Collections.Generic;
using System.Linq;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;
using Looking4AWinner.LikeUs.Components;

#endregion

namespace Looking4AWinner.LikeUs.Optimizers
{
    public class JulienAlgo : IGameOptimizer<Game, CacheUsages>
    {

        #region Public Properties

        /// <summary>
        ///     Gets the optimizer identifier.
        /// </summary>
        /// <value>
        ///     The optimizer identifier.
        /// </value>
        public OptimizerId OptimizerId
        {
            get { return OptimizerId.Schaepi; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Optimizes the specified game and returns slices.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public IEnumerable<CacheUsages> Optimize(Game game)
        {
            var cacheUsage = new CacheUsages();
            var videos = new HashSet<Video>(game.Videos);
            foreach (
                var request in
                game.Requests.Where(e => e.EndPoint.CacheLatency.Any())
                    .OrderByDescending(r => r.RequestCount * r.EndPoint.CenterLatency.Time))
            {
                RequestLatency<DataCache> cacheRequest;
                if (!SelectCache(request, out cacheRequest))
                    continue;

                var cache = cacheRequest.CastedPublisher;
                if (cache.Videos.Contains(request.Video))
                    continue;
                cache.Videos.Add(request.Video);
                if (!cacheUsage.Caches.Contains(cache))
                    cacheUsage.Caches.Add(cache);
                videos.Remove(request.Video);
            }
            foreach (var video in videos)
            {
                
            }
            return new[] {cacheUsage};
        }

        private static bool SelectCache(VideoRequest request, out RequestLatency<DataCache> cacheRequest)
        {
            cacheRequest =
                request.EndPoint.CacheLatency.Where(
                        c => c.Publisher.AcceptVideo(request.Video) && !c.Publisher.Videos.Contains(request.Video))
                    .OrderBy(r => r.Time).FirstOrDefault();
            //cacheRequest = cacheRequests.OrderBy(c => c.Publisher.Usage).FirstOrDefault();
            if (cacheRequest == null)
                return false;
            return true;
        }

        #endregion
    }
}