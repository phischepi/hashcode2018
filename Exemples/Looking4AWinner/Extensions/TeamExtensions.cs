#region Imports

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Looking4AWinner.LikeUs.Extensions {
    public static class TeamExtensions {
        #region Public Methods

        /// <summary>
        ///     To the int array.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static int[] ToIntArray(this string[] items) {
            return items.Select(int.Parse).ToArray();
        }
        /// <summary>
        /// To the int array.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="idx">The index.</param>
        /// <param name="splitter">The splitter.</param>
        /// <returns></returns>
        public static int[] ToIntArray(this IList<string> items, int idx, char splitter = ' ') {
            return items[idx].Split(splitter).ToIntArray();
        }
        /// <summary>
        /// To the int array.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="splitter">The splitter.</param>
        /// <returns></returns>
        public static int[] ToIntArray(this string items, char splitter = ' ') {
            return items.Split(splitter).ToIntArray();
        }

        #endregion
    }
}