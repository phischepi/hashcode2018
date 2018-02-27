#region Imports

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Gilgen.Utils.Google.PizzaGame.Extensions {
    public static class CombinationsHelper {
        #region Public Methods

        /// <summary>
        ///     Gets the cartesian product.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequences">The sequences.</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> GetCartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences) {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] {
                Enumerable.Empty<T>()
            };
            return sequences.Aggregate(emptyProduct, (accumulator, sequence) =>
                    accumulator.SelectMany(accseq => sequence, (accseq, item) => accseq.Concat(new[] {
                        item
                    })));
        }

        /// <summary>
        ///     Combinates the specified k.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="k">The k.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">source</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">k</exception>
        public static IEnumerable<IEnumerable<TSource>> GetCombinations<TSource>(this IEnumerable<TSource> source, int k) {
            if(source == null) {
                throw new ArgumentNullException("source");
            }

            List<TSource> list = source.ToList();
            if(k > list.Count) {
                throw new ArgumentOutOfRangeException("k");
            }

            if(k == 0) {
                yield return Enumerable.Empty<TSource>();
            }

            foreach(TSource l in list) {
                foreach(IEnumerable<TSource> c in GetCombinations(list.Skip(list.Count - k - 2), k - 1)) {
                    yield return c.Prepend(l);
                }
            }
        }

        /// <summary>
        ///     Gets the permutations.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">source</exception>
        public static IEnumerable<IEnumerable<TSource>> GetPermutations<TSource>(this IEnumerable<TSource> source) {
            if(source == null) {
                throw new ArgumentNullException("source");
            }

            List<TSource> list = source.ToList();

            if(list.Count > 1) {
                return list.SelectMany(s => GetPermutations(list.Take(list.IndexOf(s)).Concat(list.Skip(list.IndexOf(s) + 1))), (s, p) => p.Prepend(s));
            }
            return new[] {
                list
            };
        }

        /// <summary>
        ///     Converts to the cartesian products.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <returns></returns>
        public static T[][] ToCartesianProducts<T>(this IList<T> list1, IList<T> list2) {
            return GetCartesianProduct(new[] {
                list1, list2
            }).Select(e => e.ToArray()).ToArray();
        }

        /// <summary>
        ///     Converts to the combinations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="k">The k.</param>
        /// <returns></returns>
        public static T[][] ToCombinations<T>(this IList<T> list, int k) {
            return GetCombinations(list, k).Select(l => l.ToArray()).ToArray();
        }

        /// <summary>
        ///     Converts to the permutations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static T[][] ToPermutations<T>(this IList<T> list) {
            return GetPermutations(list).Select(l => l.ToArray()).ToArray();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Prepends the specified item.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">source</exception>
        private static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource item) {
            if(source == null) {
                throw new ArgumentNullException("source");
            }
            yield return item;

            foreach(TSource element in source) {
                yield return element;
            }
        }

        #endregion
    }
}