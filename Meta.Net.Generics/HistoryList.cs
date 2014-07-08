using System;
using System.Collections;
using System.Collections.Generic;
using Meta.Net.Generics.Interfaces;

namespace Meta.Net.Generics
{
    /// <summary>
    /// A class that keeps and maintains the historical index named HistoryIndex for a generic List
    /// and also exposes that list if historical search tracking is unwanted in certain situations.
    /// The HistoryIndex only gets updated for successful Find(), FindAll(), and Contains() methods
    /// and the HistoryIndex is set to the first found search item's index. This allows you to
    /// quickly search for an item again without any performance penalty. On the FindAll() method,
    /// if an item is found that matches the predicate, the algorithm stops looking for more items
    /// when an item fails to match the predicate or when the search algorithm hits the end of the list.
    /// The search algorithms work much like a linear hashing algorithm when an a match hasn't been
    /// found by the end of the List it loops back to the beginning of the List searching for
    /// items that match the predicate until it hits the HistoryIndex, which was the search start point.
    /// This is useful when searching for partial namespaces and passing in Predicate.StartsWith.
    /// </summary>
    public class HistoryList<T> : IHistoryList<T> where T : class
    {
        # region members (2)

        public int HistoryIndex { get; set; }
        public List<T> List { get; set; }

        # endregion members

        # region constructors (1)

        public HistoryList()
        {
            HistoryIndex = 0;
            List = new List<T>();
        }

        # endregion constructors

        # region properties (2)

        public T this[int index]
        {
            get
            {
                return List[index];
            }
        }

        public int Count
        {
            get
            {
                return List.Count;
            }
        }

        # endregion properties

        # region methods (8)

        public void Add(T item)
        {
            List.Add(item);
        }

        public void Remove(T item)
        {
            List.Remove(item);
            HistoryIndex = 0;
        }

        public void Clear()
        {
            HistoryIndex = 0;
            List.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        /// <summary>
        /// IEnumerable type T inherits from IEnumerable, therefore this class must implement both the
        /// generic and non-generic versions of GetEnumerator. In most cases, the non-generic method
        /// can simply call the generic method.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Uses HistoryIndex as a starting point.
        /// </summary>
        /// <param name="item">Item to check for.</param>
        /// <returns>true if success, false if epic fail.</returns>
        public bool Contains(T item)
        {
            // Search from the historical index to the end of the list.
            for (var i = HistoryIndex; i < Count; i++)
            {
                if (List[i] != item) continue;

                // Match found, update the historical index.
                HistoryIndex = i;
                return true;
            }

            // Search from the beginning of the list to the historical index.
            for (var i = 0; i < HistoryIndex; i++)
            {
                if (List[i] != (item)) continue;

                // Match found, update the historical index.
                HistoryIndex = i;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Uses HistoryIndex as a starting point.
        /// </summary>
        /// <param name="match">Predicate to match against.</param>
        /// <returns>true if success, false if epic fail.</returns>
        public bool Contains(Predicate<T> match)
        {
            // Search from the historical index to the end of the list.
            for (var i = HistoryIndex; i < Count; i++)
            {
                if (!match.Invoke(List[i])) continue;

                // Match found, update the historical index.
                HistoryIndex = i;
                return true;
            }

            // Search from the beginning of the list to the historical index.
            for (var i = 0; i < HistoryIndex; i++)
            {
                if (!match.Invoke(List[i])) continue;

                // Match found, update the historical index.
                HistoryIndex = i;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Uses HistoryIndex as a starting point.
        /// </summary>
        /// <param name="match">Predicate to match against.</param>
        /// <returns>Match results of type T, or null.</returns>
        public T Find(Predicate<T> match)
        {
            // Search from the historical index to the end of the list.
            for (var i = HistoryIndex; i < Count; i++)
            {
                if (!match.Invoke(List[i])) continue;

                // Match found, update the historical index.
                HistoryIndex = i;
                return List[i];
            }

            // Search from the beginning of the list to the historical index.
            for (var i = 0; i < HistoryIndex; i++)
            {
                if (!match.Invoke(List[i])) continue;

                // Match found, update the historical index.
                HistoryIndex = i;
                return List[i];
            }

            return null;
        }

        /// <summary>
        /// Uses HistoryIndex as a starting point.
        /// </summary>
        /// <param name="match">Predicate to match against.</param>
        /// <returns>A new match results List of type T, or an empty list.</returns>
        public HistoryList<T> FindAll(Predicate<T> match)
        {
            var list = new HistoryList<T>();
            var index = HistoryIndex;

            // Search from the historical index to the end of the list.
            for (var i = index; i < Count; i++)
            {
                if (!match.Invoke(List[i])) continue;

                // Match found, update the historical index.
                HistoryIndex = i;
                list.Add(List[i]);

                if (HistoryIndex == Count - 1) return list;

                // Gather matches until the first failed matched or until we hit the end of the list.
                for (var j = HistoryIndex + 1; j < Count; j++)
                {
                    if (!match.Invoke(List[j])) return list;
                    list.Add(List[j]);
                    if (j == Count - 1) return list;
                }
            }

            // Search from the beginning of the list to the historical index.
            for (var i = 0; i < index; i++)
            {
                if (!match.Invoke(List[i])) continue;

                // Match found, update the historical index.
                HistoryIndex = i;
                list.Add(List[i]);

                // Gather matches until the first failed matched or until we hit the
                // original historical index.
                for (var j = HistoryIndex + 1; j < index; j++)
                {
                    if (!match.Invoke(List[j])) return list;
                    list.Add(List[j]);
                    if (j == index - 1) return list;
                }
            }

            // No matches found, return empty list.
            return list;
        }

        # endregion methods
    }
}
