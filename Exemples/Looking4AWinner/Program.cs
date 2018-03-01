#region Imports

using System;
using System.Diagnostics;
using System.Linq;
using Gilgen.Utils.Common.ModuleBase.Extensions;
using Gilgen.Utils.Google.BoardGameLibrary;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;
using Looking4AWinner.LikeUs.Components;

#endregion

namespace Looking4AWinner.LikeUs {
    class Program {
        #region Public Enums

        public enum GameInputs {
            Kittens = 0,
            MeAtThezoo = 1,
            TrendingToday = 2,
            Videosworthspreading = 3,
            Unset = 4,
            Example = 5
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args) {
            //TestBoardExtensions();
            GameInputs input = GameProcessor.GetEnumFromArg(args, 0, GameInputs.Unset);
            OptimizerId optimizer = GameProcessor.GetEnumFromArg(args, 1, OptimizerId.Schaepi);
            Stopwatch totalDuration = new Stopwatch();
            totalDuration.Start();
            if(input == GameInputs.Unset) {
                GameProcessor.ComputeAll<IGameOptimizer<Game, CacheUsages>, Game, CacheUsages, GameInputs>(optimizer, EnumExtensions.GetValues<GameInputs>().ExceptElts(GameInputs.Unset).ToArray());
            } else {
                GameProcessor.Compute<IGameOptimizer<Game, CacheUsages>, Game, CacheUsages, GameInputs>(optimizer, input);
            }
            totalDuration.DisplayTime("Finished !");
            Console.ReadLine();
            Console.WriteLine("Tap to quit");
            Console.ReadLine();
        }

        #endregion
    }
}