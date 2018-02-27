#region Imports

using System;
using System.Collections.Generic;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;

#endregion

namespace Gilgen.Utils.Google.BoardGameLibrary.Extensions {
    public static class BoardExtensions {
        #region Public Methods

        /// <summary>
        ///     Gets the specified column index.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="rowIdx">Index of the row.</param>
        /// <param name="columnIdx">Index of the column.</param>
        /// <param name="rowsCount">The rows count.</param>
        /// <param name="columnsCount">The columns count.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static TItem Get<TItem>(this IList<TItem> items, int rowIdx, int columnIdx, int rowsCount, int columnsCount, TravelSource source = TravelSource.TopLeft) {
            return items[GetCellIndex(rowIdx, columnIdx, rowsCount, columnsCount, source)];
        }

        /// <summary>
        ///     Gets the specified column index.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="idx">The index.</param>
        /// <param name="rowsCount">The rows count.</param>
        /// <param name="columnsCount">The columns count.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static TItem Get<TItem>(this IList<TItem> items, int idx, int rowsCount, int columnsCount, TravelSource source = TravelSource.TopLeft) {
            return items[GetCellIndex(idx, rowsCount, columnsCount, source)];
        }

        /// <summary>
        ///     Gets the index of the ingredient.
        /// </summary>
        /// <param name="rowIdx">Index of the row.</param>
        /// <param name="columnIdx">Index of the column.</param>
        /// <param name="rowsCount">The rows count.</param>
        /// <param name="columnsCount">The columns count.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">source;null</exception>
        public static int GetCellIndex(int rowIdx, int columnIdx, int rowsCount, int columnsCount, TravelSource source = TravelSource.TopLeft) {
            switch(source) {
                case TravelSource.TopLeft:
                    return (rowIdx * columnsCount) + columnIdx;
                case TravelSource.BottomRight:
                    return ((rowsCount - 1 - rowIdx) * columnsCount) + (columnsCount - 1 - columnIdx);
                case TravelSource.TopRight:
                    return (rowIdx * columnsCount) + (columnsCount - 1 - columnIdx);
                case TravelSource.BottomLeft:
                    return ((rowsCount - 1 - rowIdx) * columnsCount) + columnIdx;
                default:
                    throw new ArgumentOutOfRangeException("source", source, null);
            }
        }

        /// <summary>
        ///     Gets the index of the ingredient.
        /// </summary>
        /// <param name="cellIdx">Index of the cell.</param>
        /// <param name="rowsCount">The rows count.</param>
        /// <param name="columnsCount">The columns count.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">source;null</exception>
        public static int GetCellIndex(int cellIdx, int rowsCount, int columnsCount, TravelSource source = TravelSource.TopLeft) {
            int rowIdx = cellIdx / columnsCount;
            int columnIdx = cellIdx % columnsCount;
            return GetCellIndex(rowIdx, columnIdx, rowsCount, columnsCount, source);
        }

        /// <summary>
        ///     Gets the column.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="columnIdx">Index of the column.</param>
        /// <param name="rowsCount">The rows count.</param>
        /// <param name="columnsCount">The columns count.</param>
        /// <param name="source">The start.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">start;null</exception>
        public static IEnumerable<TItem> GetColumn<TItem>(this IList<TItem> items, int columnIdx, int rowsCount, int columnsCount, TravelSource source = TravelSource.TopLeft) {
            for(int rowIdx = 0; rowIdx < rowsCount; rowIdx++) {
                yield return items[GetCellIndex(rowIdx, columnIdx, rowsCount, columnsCount, source)];
            }
        }

        /// <summary>
        ///     Gets the row.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="rowIdx">Index of the row.</param>
        /// <param name="rowsCount">The rows count.</param>
        /// <param name="columnsCount">The columns count.</param>
        /// <param name="source">The start.</param>
        /// <returns></returns>
        public static IEnumerable<TItem> GetRow<TItem>(this IList<TItem> items, int rowIdx, int rowsCount, int columnsCount, TravelSource source = TravelSource.TopLeft) {
            for(int columnIdx = 0; columnIdx < columnsCount; columnIdx++) {
                yield return items[GetCellIndex(rowIdx, columnIdx, rowsCount, columnsCount, source)];
            }
        }

        #endregion
    }
}