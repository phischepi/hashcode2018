namespace Gilgen.Utils.Google.BoardGameLibrary.Interfaces {
    public interface IGame {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether [source file path].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [source file path]; otherwise, <c>false</c>.
        /// </value>
        string SourceFilePath { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Reads the specified source file path.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        void Read(string sourceFilePath);

        #endregion
    }
}