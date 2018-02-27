#region Imports

using System.Collections.Generic;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;

#endregion

namespace Gilgen.Utils.Google.BoardGameLibrary.Interfaces {
    public interface IGameOptimizer<TGame, TGameOutput>
            where TGame : IGame
            where TGameOutput : IGameOutput {
        #region Public Properties

        /// <summary>
        /// Gets the optimizer identifier.
        /// </summary>
        /// <value>
        /// The optimizer identifier.
        /// </value>
        OptimizerId OptimizerId { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Optimizes the specified game and returns slices.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        IEnumerable<TGameOutput> Optimize(TGame game);

        #endregion
    }
}