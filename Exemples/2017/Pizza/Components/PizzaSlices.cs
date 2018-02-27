#region Imports

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gilgen.Utils.Google.BoardGameLibrary.Interfaces;

#endregion

namespace Gilgen.Utils.Google.PizzaGame.Components {
    public class PizzaSlices : IGameOutput {
        #region Public Properties

        /// <summary>
        ///     Gets the slices.
        /// </summary>
        /// <value>
        ///     The slices.
        /// </value>
        public List<PizzaSlice> Slices { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PizzaSlices" /> class.
        /// </summary>
        /// <param name="slices">The slices.</param>
        public PizzaSlices(IEnumerable<PizzaSlice> slices)
                : this(slices.ToArray()) {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PizzaSlices" /> class.
        /// </summary>
        /// <param name="slices">The slices.</param>
        public PizzaSlices(params PizzaSlice[] slices) {
            Slices = new List<PizzaSlice>(slices);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Exports the specified target file path.
        /// </summary>
        /// <param name="targetFilePath">The target file path.</param>
        public void Export(string targetFilePath) {
            File.WriteAllLines(targetFilePath, new[] {
                Slices.Count.ToString()
            }.Concat(Slices.Select(s => s.ToString())));
        }

        /// <summary>
        ///     Gets the result.
        /// </summary>
        /// <returns></returns>
        public int GetResult() {
            return Slices.Any() ? Slices.Sum(s => s.GetCells().Length) : 0;
        }

        #endregion
    }
}