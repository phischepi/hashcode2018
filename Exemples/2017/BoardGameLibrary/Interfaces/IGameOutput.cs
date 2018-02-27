namespace Gilgen.Utils.Google.BoardGameLibrary.Interfaces {
    public interface IGameOutput {
        #region Public Methods

        /// <summary>
        ///     Exports the specified target file path.
        /// </summary>
        /// <param name="targetFilePath">The target file path.</param>
        void Export(string targetFilePath);

        /// <summary>
        ///     Gets the result.
        /// </summary>
        /// <returns></returns>
        int GetResult();

        #endregion
    }
}