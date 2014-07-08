using System;
using System.Collections.Generic;

namespace Meta.Net.Generics.Interfaces
{
    /// <summary>
    /// Interface for a historical generic list.
    /// </summary>
    public interface IHistoryList<T> : IEnumerable<T> where T : class
    {
        int HistoryIndex { get; }
        List<T> List { get; }
        T this[int index] { get; }
        int Count { get; }
        void Add(T item);
        void Remove(T item);
        void Clear();
        new IEnumerator<T> GetEnumerator();
        bool Contains(T item);
        bool Contains(Predicate<T> match);
        T Find(Predicate<T> match);
        HistoryList<T> FindAll(Predicate<T> match);
    }
}
