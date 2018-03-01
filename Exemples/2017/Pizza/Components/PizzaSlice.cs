#region Imports

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Gilgen.Utils.Google.BoardGameLibrary.Enums;
using Gilgen.Utils.Google.PizzaGame.Extensions;

#endregion

namespace Gilgen.Utils.Google.PizzaGame.Components {
    public class PizzaSlice {
        #region Private Fields

        private PizzaCell _bottomLeftCell;
        private PizzaCell _bottomRightCell;
        private readonly ObservableCollection<PizzaCell> _cells;
        private PizzaCell[] _cellsArray;
        private PizzaCell _topLeftCell;
        private PizzaCell _topRightCell;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the cells.
        /// </summary>
        /// <value>
        ///     The cells.
        /// </value>
        public ObservableCollection<PizzaCell> Cells {
            get {
                return _cells;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PizzaSlice" /> class.
        /// </summary>
        public PizzaSlice() {
            _cells = new ObservableCollection<PizzaCell>();
            _cells.CollectionChanged += (sender, args) => { InitCache(); };
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the bottom left point.
        /// </summary>
        /// <returns></returns>
        public PizzaCell GetBottomLeftCell() {
            return _bottomLeftCell ?? (_bottomLeftCell = _cells.GetBottomLeft());
        }

        /// <summary>
        ///     Gets the bottom right point.
        /// </summary>
        /// <returns></returns>
        public PizzaCell GetBottomRightCell() {
            return _bottomRightCell ?? (_bottomRightCell = _cells.GetBottomRight());
        }

        /// <summary>
        ///     Gets the cell.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public PizzaCell GetCell(TravelSource source) {
            switch(source) {
                case TravelSource.TopLeft:
                    return GetTopLeftCell();
                case TravelSource.BottomRight:
                    return GetBottomRightCell();
                case TravelSource.TopRight:
                    return GetTopRightCell();
                case TravelSource.BottomLeft:
                    return GetBottomLeftCell();
                default:
                    throw new ArgumentOutOfRangeException("source", source, null);
            }
        }

        /// <summary>
        ///     Gets the cells.
        /// </summary>
        /// <returns></returns>
        public PizzaCell[] GetCells() {
            return _cellsArray ?? (_cellsArray = _cells.ToArray());
        }

        /// <summary>
        ///     Gets the cells.
        /// </summary>
        /// <returns></returns>
        public int GetCellsCount() {
            return _cells.Count;
        }

        /// <summary>
        ///     Gets the top left point.
        /// </summary>
        /// <returns></returns>
        public PizzaCell GetTopLeftCell() {
            return _topLeftCell ?? (_topLeftCell = _cells.GetTopLeft());
        }

        /// <summary>
        ///     Gets the top right point.
        /// </summary>
        /// <returns></returns>
        public PizzaCell GetTopRightCell() {
            return _topRightCell ?? (_topRightCell = _cells.GetTopRight());
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString() {
            PizzaCell pizzaCellA = GetTopLeftCell();
            PizzaCell pizzaCellB = GetBottomRightCell();
            return string.Format("{0} {1} {2} {3}", pizzaCellA.X, pizzaCellA.X, pizzaCellB.X, pizzaCellB.Y);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Initializes the cache.
        /// </summary>
        private void InitCache() {
            _cellsArray = null;
            _bottomLeftCell = null;
            _bottomRightCell = null;
            _topLeftCell = null;
            _topRightCell = null;
        }

        #endregion
    }
}