#region Imports

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;
using Looking4AWinner.LikeUs.Components;

#endregion

namespace Looking4AWinner.LikeUs.Optimizers {
    public class SebAlgo : IGameOptimizer<Game, CacheUsages> {
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

        private Random _rnd = new Random();

        #region Public Methods

        /// <summary>
        ///     Optimizes the specified game and returns slices.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public IEnumerable<CacheUsages> Optimize(Game game)
        {
            
            {
                return new List<CacheUsages>();
            }
        }
        




        #endregion
    }
}