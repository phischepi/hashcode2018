#region Imports

using Gilgen.Utils.Common.ModuleBase.Implementations;

#endregion

namespace Looking4AWinner.LikeUs.Components {
    public abstract class VideoPublisher : Identifiable {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        public int Size { get; private set; }

        /// <summary>
        ///     Gets or sets the space left.
        /// </summary>
        /// <value>
        ///     The space left.
        /// </value>
        public int SpaceLeft { get; set; }

        /// <summary>
        ///     Gets or sets the usage.
        /// </summary>
        /// <value>
        ///     The usage.
        /// </value>
        public int Usage { get; set; }

        /// <summary>
        ///     Gets the videos.
        /// </summary>
        /// <value>
        ///     The videos.
        /// </value>
        public VerboseObservableCollection<Video> Videos { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VideoPublisher" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        protected VideoPublisher(int id, int size) {
            Id = id;
            Size = size;
            SpaceLeft = size;
            Videos = new VerboseObservableCollection<Video>();
            Videos.CollectionItemAdded += (sender, args) => {
                Usage += args.Tag.Size;
                SpaceLeft -= args.Tag.Size;
            };
            Videos.CollectionItemRemoved += (sender, args) => {
                Usage -= args.Tag.Size;
                SpaceLeft += args.Tag.Size;
            };
            Videos.Cleared += (sender, args) => {
                Usage = 0;
                SpaceLeft = Size;
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Accepts the video.
        /// </summary>
        /// <param name="video">The video.</param>
        /// <returns></returns>
        public bool AcceptVideo(Video video) {
            return SpaceLeft >= video.Size;
        }

        #endregion
    }
}