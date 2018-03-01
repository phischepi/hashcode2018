namespace Looking4AWinner.LikeUs.Components {
    public class Video : Identifiable {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the size in MB.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        public int Size { get; set; }

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
        ///     Initializes a new instance of the <see cref="Video" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        public Video(int id, int size) {
            Id = id;
            Size = size;
            Requests = new VideoRequest[0];
        }

        #endregion
    }
}