#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Gilgen.Utils.Common.ModuleBase.Extensions;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;

#endregion

namespace Gilgen.Utils.Google.BoardGameLibrary {
    public static class GameProcessor {
        #region Public Methods

        /// <summary>
        ///     Computes the specified input.
        /// </summary>
        /// <typeparam name="TGameOptimizer">The type of the game optimizer.</typeparam>
        /// <typeparam name="TGame">The type of the game.</typeparam>
        /// <typeparam name="TGameOutput">The type of the game output.</typeparam>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="input">The input.</param>
        /// <param name="optmizer">The optmizer.</param>
        public static void Compute<TGameOptimizer, TGame, TGameOutput, TInput>(TInput input, OptimizerId optmizer)
                where TGameOptimizer : IGameOptimizer<TGame, TGameOutput>
                where TGame : IGame, new()
                where TGameOutput : IGameOutput {
            TGameOptimizer optimizer = GetOptimizer<TGameOptimizer, TGame, TGameOutput>(optmizer);
            Compute<TGameOptimizer, TGame, TGameOutput, TInput>(input, optimizer);
        }

        /// <summary>
        ///     Computes all.
        /// </summary>
        /// <typeparam name="TGameOptimizer">The type of the game optimizer.</typeparam>
        /// <typeparam name="TGame">The type of the game.</typeparam>
        /// <typeparam name="TGameOutput">The type of the game output.</typeparam>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="optmizer">The optmizer.</param>
        public static void ComputeAll<TGameOptimizer, TGame, TGameOutput, TInput>(OptimizerId optmizer)
                where TGameOptimizer : IGameOptimizer<TGame, TGameOutput>
                where TGame : IGame, new()
                where TGameOutput : IGameOutput {
            TGameOptimizer optimizer = GetOptimizer<TGameOptimizer, TGame, TGameOutput>(optmizer);
            TInput[] inputs = EnumExtensions.GetValues<TInput>();
            foreach(TInput input in inputs) {
                Compute<TGameOptimizer, TGame, TGameOutput, TInput>(input, optimizer);
            }
        }

        /// <summary>
        ///     Displays the time.
        /// </summary>
        /// <param name="watch">The watch.</param>
        /// <param name="title">The title.</param>
        /// <param name="forceRestart">if set to <c>true</c> [force restart].</param>
        public static void DisplayTime(this Stopwatch watch, string title, bool forceRestart = false) {
            TimeSpan duration = watch.Elapsed;
            Console.WriteLine("{3} :{0}m {1}s {2}mm", (int) duration.TotalMinutes, duration.Seconds, duration.Milliseconds, title);
            if(forceRestart) {
                watch.Restart();
            }
        }

        /// <summary>
        ///     Gets the input.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="args">The arguments.</param>
        /// <param name="idx">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TInput GetEnumFromArg<TInput>(string[] args, int idx, TInput defaultValue = default(TInput)) {
            string value = args.Skip(idx).Take(1).FirstOrDefault();
            if(string.IsNullOrEmpty(value)) {
                return defaultValue;
            }
            return args.Skip(idx).Take(1).Select(e => {
                int algoInput;
                if(int.TryParse(e, out algoInput)) {
                    return EnumExtensions.GetValue(algoInput, defaultValue);
                }
                return defaultValue;
            }).FirstOrDefault();
        }

        /// <summary>
        ///     Gets the optimizer.
        /// </summary>
        /// <typeparam name="TGameOptimizer">The type of the game optimizer.</typeparam>
        /// <typeparam name="TGame">The type of the game.</typeparam>
        /// <typeparam name="TGameOutput">The type of the game output.</typeparam>
        /// <param name="optmizerId">The optmizer identifier.</param>
        /// <returns></returns>
        public static TGameOptimizer GetOptimizer<TGameOptimizer, TGame, TGameOutput>(OptimizerId optmizerId)
                where TGameOptimizer : IGameOptimizer<TGame, TGameOutput>
                where TGame : IGame, new()
                where TGameOutput : IGameOutput {
            Type optimizerInterface = typeof(TGameOptimizer);
            IEnumerable<Type> types = Assembly.GetEntryAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(optimizerInterface));
            IEnumerable<TGameOptimizer> optimizers = types.Select(Activator.CreateInstance).OfType<TGameOptimizer>();
            TGameOptimizer optimizer = optimizers.FirstOrDefault(o => o.OptimizerId == optmizerId);
            return optimizer;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Computes the specified input.
        /// </summary>
        /// <typeparam name="TGameOptimizer">The type of the game optimizer.</typeparam>
        /// <typeparam name="TGame">The type of the game.</typeparam>
        /// <typeparam name="TGameOutput">The type of the game output.</typeparam>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="input">The input.</param>
        /// <param name="optimizer">The optimizer.</param>
        private static void Compute<TGameOptimizer, TGame, TGameOutput, TInput>(TInput input, TGameOptimizer optimizer)
                where TGameOptimizer : IGameOptimizer<TGame, TGameOutput>
                where TGame : IGame, new()
                where TGameOutput : IGameOutput {
            Console.WriteLine("Working with algo #{0}", optimizer.GetType().Name);
            string directory = Path.GetDirectoryName(optimizer.GetType().Assembly.Location) ?? string.Empty;
            string sourceFilePath = Path.Combine(directory, "InputFiles", string.Format("{0}.in", input.ToString().ToLower()));
            Console.WriteLine("on input {0} (file: {1})", input, sourceFilePath);

            TGame game = new TGame();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Stopwatch totalDuration = new Stopwatch();
            totalDuration.Start();
            Console.WriteLine("Reading...");
            game.Read(sourceFilePath);
            watch.DisplayTime("Read !", true);
            int idx = 1;
            foreach(TGameOutput gameOutput in optimizer.Optimize(game)) {
                watch.DisplayTime(string.Format("Optimized #{0}", idx), true);
                string targetFilePath = Path.Combine(directory, string.Format("{0}.{1}.{2}_{3}.out", input.ToString().ToLower(), optimizer.OptimizerId, idx, DateTime.Now));
                gameOutput.Export(targetFilePath);
                watch.DisplayTime(string.Format("Exported #{0}", idx), true);
                watch.DisplayTime(string.Format("GetResult ! result = {0} : ", gameOutput.GetResult()), true);
                idx++;
            }
            totalDuration.DisplayTime(string.Format("Done {0}!", input));
        }

        #endregion
    }
}