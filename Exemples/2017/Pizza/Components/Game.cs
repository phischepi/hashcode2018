#region Imports

using System.IO;
using System.Linq;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;
using Gilgen.Utils.Google.PizzaGame.Enums;

#endregion

namespace Gilgen.Utils.Google.PizzaGame.Components {
    public class Game : IGame {
        #region Public Properties

        /// <summary>
        ///     Gets the maximum cells per slice.
        /// </summary>
        /// <value>
        ///     The maximum cells per slice.
        /// </value>
        public int MaxCellsPerSlice { get; private set; }

        /// <summary>
        ///     Gets the minimum ingredient per slice.
        /// </summary>
        /// <value>
        ///     The minimum ingredient per slice.
        /// </value>
        public int MinIngredientPerSlice { get; private set; }

        /// <summary>
        ///     Gets the pizza.
        /// </summary>
        /// <value>
        ///     The pizza.
        /// </value>
        public Pizza Pizza { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether [source file path].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [source file path]; otherwise, <c>false</c>.
        /// </value>
        public string SourceFilePath { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Reads the specified source file path.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        public void Read(string sourceFilePath) {
            string[] lines = File.ReadAllLines(sourceFilePath);
            string[] baseRules = lines[0].Split(' ');
            MinIngredientPerSlice = int.Parse(baseRules[2]);
            MaxCellsPerSlice = int.Parse(baseRules[3]);
            PizzaCell[] cells = lines.Skip(1).SelectMany((l, y) => l.Select((c, x) => new PizzaCell(x, y, (Ingredient) c))).ToArray();
            Pizza = new Pizza(int.Parse(baseRules[0]), int.Parse(baseRules[1]), cells);
            SourceFilePath = sourceFilePath;
        }

        #endregion
    }
}