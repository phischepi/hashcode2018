#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.PizzaGame.Components;

#endregion

namespace Gilgen.Utils.Google.PizzaGame.Extensions {
    public static class PizzaCellExtensions {
        #region Public Methods

        /// <summary>
        ///     Gets the top right point.
        /// </summary>
        /// <param name="cells">The cells.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static PizzaCell Get(this IEnumerable<PizzaCell> cells, TravelSource source) {
            return cells.OrderFrom(source).First();
        }

        /// <summary>
        ///     Gets the bottom left point.
        /// </summary>
        /// <returns></returns>
        public static PizzaCell GetBottomLeft(this IEnumerable<PizzaCell> cells) {
            return cells.Get(TravelSource.BottomLeft);
        }

        /// <summary>
        ///     Gets the bottom right point.
        /// </summary>
        /// <returns></returns>
        public static PizzaCell GetBottomRight(this IEnumerable<PizzaCell> cells) {
            return cells.Get(TravelSource.BottomRight);
        }

        /// <summary>
        ///     Gets the top left point.
        /// </summary>
        /// <returns></returns>
        public static PizzaCell GetTopLeft(this IEnumerable<PizzaCell> cells) {
            return cells.Get(TravelSource.TopLeft);
        }

        /// <summary>
        ///     Gets the top right point.
        /// </summary>
        /// <returns></returns>
        public static PizzaCell GetTopRight(this IEnumerable<PizzaCell> cells) {
            return cells.Get(TravelSource.TopRight);
        }

        /// <summary>
        ///     Return cells orders from.
        /// </summary>
        /// <param name="cells">The cells.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IEnumerable<PizzaCell> OrderFrom(this IEnumerable<PizzaCell> cells, TravelSource source) {
            switch(source) {
                case TravelSource.TopLeft:
                    return cells.OrderBy(c => c.Y).ThenBy(c => c.X);
                case TravelSource.BottomRight:
                    return cells.OrderByDescending(c => c.Y).ThenByDescending(c => c.X);
                case TravelSource.TopRight:
                    return cells.OrderBy(c => c.Y).ThenByDescending(c => c.X);
                case TravelSource.BottomLeft:
                    return cells.OrderByDescending(c => c.Y).ThenBy(c => c.X);
                default:
                    throw new ArgumentOutOfRangeException("source", source, null);
            }
        }

        #endregion
    }
}