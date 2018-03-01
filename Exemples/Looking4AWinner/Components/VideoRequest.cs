namespace Looking4AWinner.LikeUs.Components
{
    public class VideoRequest
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the end point.
        /// </summary>
        /// <value>
        ///     The end point.
        /// </value>
        public VideoEndpoint EndPoint { get; set; }

        /// <summary>
        ///     Gets or sets the request count.
        /// </summary>
        /// <value>
        ///     The request count.
        /// </value>
        public int RequestCount { get; set; }

        /// <summary>
        ///     Gets or sets the video.
        /// </summary>
        /// <value>
        ///     The video.
        /// </value>
        public Video Video { get; set; }

        #endregion
    }
}