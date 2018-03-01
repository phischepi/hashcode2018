namespace Looking4AWinner.LikeUs.Components {
    public class DataCenter : VideoPublisher {
        #region Public Constants

        /// <summary>
        ///     The data center identifier
        /// </summary>
        public const int DataCenterId = -1;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VideoPublisher" /> class.
        /// </summary>
        public DataCenter()
                : base(DataCenterId, int.MaxValue) {
        }

        #endregion
    }
}