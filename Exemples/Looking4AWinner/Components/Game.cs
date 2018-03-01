#region Imports

using System.IO;
using System.Linq;
using Gilgen.Utils.Common.ModuleBase.Extensions;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;
using Looking4AWinner.LikeUs.Extensions;

#endregion

namespace Looking4AWinner.LikeUs.Components {
    public class Game : IGame {
        #region Public Properties

        /// <summary>
        ///     Gets the caches.
        /// </summary>
        /// <value>
        ///     The caches.
        /// </value>
        public DataCache[] Caches { get; private set; }

        /// <summary>
        ///     Gets the data center.
        /// </summary>
        /// <value>
        ///     The data center.
        /// </value>
        public DataCenter DataCenter { get; private set; }

        /// <summary>
        ///     Gets the end points.
        /// </summary>
        /// <value>
        ///     The end points.
        /// </value>
        public VideoEndpoint[] EndPoints { get; private set; }

        /// <summary>
        ///     Gets the requests.
        /// </summary>
        /// <value>
        ///     The requests.
        /// </value>
        public VideoRequest[] Requests { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether [source file path].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [source file path]; otherwise, <c>false</c>.
        /// </value>
        public string SourceFilePath { get; private set; }

        /// <summary>
        ///     Gets the videos.
        /// </summary>
        /// <value>
        ///     The videos.
        /// </value>
        public Video[] Videos { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets all publishers.
        /// </summary>
        /// <returns></returns>
        public VideoPublisher[] GetAllPublishers() {
            return new VideoPublisher[] {
                DataCenter
            }.Concat(Caches).ToArray();
        }

        /// <summary>
        ///     Gets the cache.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public VideoPublisher GetCache(int id) {
            return Caches.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        ///     Gets the end point.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public VideoEndpoint GetEndPoint(int id) {
            return EndPoints.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        ///     Gets the video.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Video GetVideo(int id) {
            return Videos.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        ///     Reads the specified source file path.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        public void Read(string sourceFilePath) {
            string[] lines = File.ReadAllLines(sourceFilePath);
            int[] descriptions = lines.ToIntArray(0);
            int videosCount = descriptions[0];
            int[] videosSizes = lines.ToIntArray(1);
            Videos = videosCount.ForEach(v => new Video(v, videosSizes[v])).ToArray();

            int cachesCount = descriptions[3];
            int cachesSize = descriptions[4];
            Caches = cachesCount.ForEach(e => new DataCache(e, cachesSize)).ToArray();

            DataCenter = new DataCenter();

            int endpointsCount = descriptions[1];
            EndPoints = endpointsCount.ForEach(e => new VideoEndpoint(e)).ToArray();

            int requestsCount = descriptions[2];
            Requests = requestsCount.ForEach(e => new VideoRequest()).ToArray();

            string[] linesLeft = lines.Skip(2).ToArray();
            int lineIdx = 0;
            for(int idx = 0; idx < EndPoints.Length; idx++) {
                VideoEndpoint endpoint = EndPoints[idx];
                int[] values = linesLeft.ToIntArray(lineIdx);
                int dataCenterLatency = values[0];
                endpoint.CenterLatency = new RequestLatency<DataCenter>() {
                    Publisher = DataCenter,
                    Time = dataCenterLatency
                };
                int connectionsCount = values[1];
                RequestLatency<DataCache>[] latencies = connectionsCount.ForEach(c => new RequestLatency<DataCache>()).ToArray();
                lineIdx++;
                for(int i = 0; i < latencies.Length; i++) {
                    values = linesLeft.ToIntArray(lineIdx);
                    RequestLatency<DataCache> latency = latencies[i];
                    latency.Publisher = GetCache(values[0]);
                    latency.Time = values[1];
                    lineIdx++;
                }
                endpoint.CacheLatency = latencies;
            }
            linesLeft = linesLeft.Skip(lineIdx).ToArray();
            Requests = linesLeft.Length.ForEach(e => new VideoRequest()).ToArray();
            for(int i = 0; i < linesLeft.Length; i++) {
                string line = linesLeft[i];
                int[] values = line.ToIntArray();
                Requests[i].Video = GetVideo(values[0]);
                Requests[i].Video.Requests = Requests[i].Video.Requests.ConcatElt(Requests[i]).ToArray();
                Requests[i].EndPoint = GetEndPoint(values[1]);
                Requests[i].EndPoint.Requests = Requests[i].EndPoint.Requests.ConcatElt(Requests[i]).ToArray();
                Requests[i].RequestCount = values[2];
            }

            DataCenter.Videos.AddRange(Videos);

            SourceFilePath = sourceFilePath;
        }

        /// <summary>
        ///     Reloads the specified source file path.
        /// </summary>
        public void Reload() {
            Read(SourceFilePath);
        }

        #endregion
    }
}