#region Imports

using System.Collections.Generic;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;
using Gilgen.Utils.Google.PizzaGame.Components;

#endregion

namespace Gilgen.Utils.Google.PizzaGame.Optimizers {
    public class SebAlgo : IGameOptimizer<Game, PizzaSlices> {
        #region Public Properties

        /// <summary>
        ///     Gets the optimizer identifier.
        /// </summary>
        /// <value>
        ///     The optimizer identifier.
        /// </value>
        public OptimizerId OptimizerId {
            get {
                return OptimizerId.Seb;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Optimizes the specified game and returns slices.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public IEnumerable<PizzaSlices> Optimize(Game game) {
            return new PizzaSlices[0];
        }

        #endregion
    }
}