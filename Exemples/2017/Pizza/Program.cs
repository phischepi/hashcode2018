#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Gilgen.Utils.Common.ModuleBase.Extensions;
using Gilgen.Utils.Google.BoardGameLibrary;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.BoardGameLibrary.Extensions;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;
using Gilgen.Utils.Google.PizzaGame.Components;

#endregion

namespace Gilgen.Utils.Google.PizzaGame {
    internal class Program {
        #region Public Enums

        public enum GameInputs {
            Example = 0,
            Small = 1,
            Medium = 2,
            Big = 3,
            Unset = 4
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
            OptimizerId optimizer = GameProcessor.GetEnumFromArg(args, 1, OptimizerId.Schepi);
            //OptimizerId optimizer = GameProcessor.GetEnumFromArg(args, 1, OptimizerId.Jon);
            Stopwatch totalDuration = new Stopwatch();
            totalDuration.Start();
            if(input == GameInputs.Unset) {
                GameProcessor.ComputeAll<IGameOptimizer<Game, PizzaSlices>, Game, PizzaSlices, GameInputs>(optimizer);
            } else {
                GameProcessor.Compute<IGameOptimizer<Game, PizzaSlices>, Game, PizzaSlices, GameInputs>(input, optimizer);
            }
            totalDuration.DisplayTime("Finished !");
            Console.ReadLine();
            Console.WriteLine("Tap to quit");
            Console.ReadLine();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Tests the board extensions.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private static void TestBoardExtensions() {
            TravelSource[] sources = EnumExtensions.GetValues<TravelSource>();
            foreach(TravelSource travelSource in sources) {
                Console.WriteLine(travelSource);
                const int rowsCount = 3, columnsCount = 5;
                List<int> values = new List<int>();
                for(int rowIdx = 0, idx = 0; rowIdx < rowsCount; rowIdx++) {
                    for(int columnIdx = 0; columnIdx < columnsCount; columnIdx++) {
                        int value1 = BoardExtensions.GetCellIndex(rowIdx, columnIdx, rowsCount, columnsCount, travelSource);
                        int value2 = BoardExtensions.GetCellIndex(idx, rowsCount, columnsCount, travelSource);
                        if(value1 != value2) {
                            Console.WriteLine("Error {0} - {1}", value1, value2);
                        } else if(value1 < 0 || value1 >= rowsCount * columnsCount) {
                            Console.WriteLine(value1);
                        } else {
                            Console.WriteLine(value1);
                            values.AddIfUnknown(value1);
                        }
                        idx++;
                    }
                }
                Console.WriteLine("--- {0}", values.Count == rowsCount * columnsCount);
            }
        }

        #endregion
    }
}