#region Imports

using System.Collections.Generic;
using Gilgen.Utils.Common.ModuleBase.Extensions;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;
using Looking4AWinner.LikeUs.Components;

#endregion

namespace Looking4AWinner.LikeUs.Optimizers {
    public class JonAlgo : IGameOptimizer<Game, CacheUsages> {
        #region Private Constants

        private static readonly TravelSource[] _directions = EnumExtensions.GetValues<TravelSource>();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the optimizer identifier.
        /// </summary>
        /// <value>
        ///     The optimizer identifier.
        /// </value>
        public OptimizerId OptimizerId {
            get {
                return OptimizerId.Jon;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Optimizes the specified game and returns slices.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public IEnumerable<CacheUsages> Optimize(Game game) {
            return new CacheUsages[0];
        }

        #endregion
    }
}