#region Imports

using System.Linq;

#endregion

namespace Looking4AWinner.LikeUs.Components {
    public class VideoEndpoint : Identifiable {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the data cache latency.
        /// </summary>
        /// <value>
        ///     The data cache latency.
        /// </value>
        public RequestLatency<DataCache>[] CacheLatency { get; set; }

        /// <summary>
        ///     Gets or sets the data center latency.
        /// </summary>
        /// <value>
        ///     The data center latency.
        /// </value>
        public RequestLatency<DataCenter> CenterLatency { get; set; }

        /// <summary>
        ///     Gets or sets the requests.
        /// </summary>
        /// <value>
        ///     The requests.
        /// </value>
        public VideoRequest[] Requests { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VideoEndpoint" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public VideoEndpoint(int id) {
            Id = id;
            Requests = new VideoRequest[0];
            CacheLatency = new RequestLatency<DataCache>[0];
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the cache latency.
        /// </summary>
        /// <param name="dataCache">The data cache.</param>
        /// <returns></returns>
        public RequestLatency<DataCache> GetCacheLatency(DataCache dataCache) {
            return CacheLatency.FirstOrDefault(c => c.CastedPublisher == dataCache);
        }

        /// <summary>
        ///     Gets the cache latency time.
        /// </summary>
        /// <param name="dataCache">The data cache.</param>
        /// <returns></returns>
        public int? GetCacheLatencyTime(DataCache dataCache) {
            RequestLatency<DataCache> latency = GetCacheLatency(dataCache);
            return latency == null ? (int?) null : latency.Time;
        }

        #endregion
    }
}