#region Imports

using Gilgen.Utils.Google.PizzaGame.Enums;

#endregion

namespace Gilgen.Utils.Google.PizzaGame.Components {
    public class PizzaCell {
        #region Public Properties

        /// <summary>
        ///     Gets the ingredient.
        /// </summary>
        /// <value>
        ///     The ingredient.
        /// </value>
        public Ingredient Ingredient { get; private set; }

        /// <summary>
        ///     Gets the x.
        /// </summary>
        /// <value>
        ///     The x.
        /// </value>
        public int X { get; private set; }

        /// <summary>
        ///     Gets the y.
        /// </summary>
        /// <value>
        ///     The y.
        /// </value>
        public int Y { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PizzaCell" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="ingredient">The ingredient.</param>
        public PizzaCell(int x, int y, Ingredient ingredient) {
            X = x;
            Y = y;
            Ingredient = ingredient;
        }

        #endregion
    }
}