namespace Looking4AWinner.LikeUs.Components {
    public class RequestLatency<TType>
            where TType : VideoPublisher {
        #region Public Properties

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        /// <value>
        /// The publisher.
        /// </value>
        public TType CastedPublisher { get; set; }

        /// <summary>
        ///     Gets or sets the publisher.
        /// </summary>
        /// <value>
        ///     The publisher.
        /// </value>
        public VideoPublisher Publisher {
            get {
                return CastedPublisher;
            }
            set {
                CastedPublisher = (TType) value;
            }
        }

        /// <summary>
        ///     Gets or sets the time in MS.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        public int Time { get; set; }

        #endregion
    }
}