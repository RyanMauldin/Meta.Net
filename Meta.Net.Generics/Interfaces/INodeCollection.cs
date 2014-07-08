using System.Collections.Generic;

namespace Meta.Net.Generics.Interfaces
{
    public interface INodeCollection<T> : IEnumerable<Node<T>> where T : class
    {
        Node<T> Owner { get; }
        List<Node<T>> List { get; }
        int Count { get; }
        void Add(Node<T> node);
        void Remove(Node<T> node);
        bool Contains(Node<T> node);
        void Clear();
        Node<T> this[int index] { get; }
        new IEnumerator<Node<T>> GetEnumerator();
    }
}
