#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.BoardGameLibrary.Extensions;
using Gilgen.Utils.Google.PizzaGame.Enums;

#endregion

namespace Gilgen.Utils.Google.PizzaGame.Components {
    public class Pizza : IEnumerable<PizzaCell> {
        #region Private Fields

        private PizzaCell[] _bottomLeft;
        private PizzaCell[] _bottomRight;
        private PizzaCell[] _topRight;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the cells.
        /// </summary>
        /// <value>
        ///     The cells.
        /// </value>
        public PizzaCell[] Cells { get; private set; }

        /// <summary>
        ///     Gets the columns count.
        /// </summary>
        /// <value>
        ///     The columns count.
        /// </value>
        public int ColumnsCount { get; private set; }

        /// <summary>
        ///     Gets the rows count.
        /// </summary>
        /// <value>
        ///     The rows count.
        /// </value>
        public int RowsCount { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Pizza" /> class.
        /// </summary>
        /// <param name="rowsCount">The rows count.</param>
        /// <param name="columnsCount">The columns count.</param>
        /// <param name="cells">The cells.</param>
        public Pizza(int rowsCount, int columnsCount, PizzaCell[] cells) {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            Cells = cells;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the cell.
        /// </summary>
        /// <param name="rowIdx">Index of the row.</param>
        /// <param name="columnIdx">Index of the column.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public PizzaCell GetCell(int rowIdx, int columnIdx, TravelSource source = TravelSource.TopLeft) {
            return Cells.Get(rowIdx, columnIdx, RowsCount, ColumnsCount, source);
        }

        /// <summary>
        ///     Gets the cell.
        /// </summary>
        /// <param name="cellIdx">Index of the cell.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public PizzaCell GetCell(int cellIdx, TravelSource source = TravelSource.TopLeft) {
            return Cells.Get(cellIdx, RowsCount, ColumnsCount, source);
        }

        /// <summary>
        ///     Gets the index of the ingredient.
        /// </summary>
        /// <param name="rowIdx">Index of the row.</param>
        /// <param name="columnIdx">Index of the column.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public int GetCellIndex(int rowIdx, int columnIdx, TravelSource source) {
            return BoardExtensions.GetCellIndex(rowIdx, columnIdx, ColumnsCount, RowsCount, source);
        }

        /// <summary>
        ///     Gets the index of the ingredient.
        /// </summary>
        /// <param name="cellIdx">Index of the cell.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">source;null</exception>
        public int GetCellIndex(int cellIdx, TravelSource source) {
            return BoardExtensions.GetCellIndex(cellIdx, ColumnsCount, RowsCount, source);
        }

        /// <summary>
        ///     Gets the cells.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">source;null</exception>
        public PizzaCell[] GetCells(TravelSource source = TravelSource.TopLeft) {
            switch(source) {
                case TravelSource.TopLeft:
                    return Cells;
                case TravelSource.BottomRight:
                    return _bottomRight ?? (_bottomRight = GetEnumerator(source).ToArray());
                case TravelSource.TopRight:
                    return _topRight ?? (_topRight = GetEnumerator(source).ToArray());
                case TravelSource.BottomLeft:
                    return _bottomLeft ?? (_bottomLeft = GetEnumerator(source).ToArray());
                default:
                    throw new ArgumentOutOfRangeException("source", source, null);
            }
        }

        /// <summary>
        ///     Gets the column.
        /// </summary>
        /// <param name="columnIdx">Index of the column.</param>
        /// <param name="source">The start.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">start;null</exception>
        public IEnumerable<PizzaCell> GetColumn(int columnIdx, TravelSource source = TravelSource.TopLeft) {
            return Cells.GetColumn(columnIdx, RowsCount, ColumnsCount, source);
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<PizzaCell> GetEnumerator() {
            return ((IEnumerable<PizzaCell>) Cells).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="skip">The skip.</param>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerable<PizzaCell> GetEnumerator(TravelSource source, int skip = 0) {
            for(int idx = skip; idx < Cells.Length; idx++) {
                yield return Cells[GetCellIndex(idx, source)];
            }
            /*for(int rowIdx = 0; rowIdx < Rows; rowIdx++) {
                for(int columnIdx = 0; columnIdx < Columns; columnIdx++) {
                    yield return Cells[GetCellIndex(rowIdx, columnIdx, source)];
                }
            }*/
        }

        /// <summary>
        ///     Gets the cell.
        /// </summary>
        /// <param name="rowIdx">Index of the row.</param>
        /// <param name="columnIdx">Index of the column.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public Ingredient GetIngredient(int rowIdx, int columnIdx, TravelSource source = TravelSource.TopLeft) {
            return GetCell(rowIdx, columnIdx, source).Ingredient;
        }

        /// <summary>
        ///     Gets the row.
        /// </summary>
        /// <param name="rowIdx">Index of the row.</param>
        /// <param name="source">The start.</param>
        /// <returns></returns>
        public IEnumerable<PizzaCell> GetRow(int rowIdx, TravelSource source = TravelSource.TopLeft) {
            return Cells.GetRow(rowIdx, RowsCount, ColumnsCount, source);
        }

        #endregion

        #region Private Methods

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion
    }
}