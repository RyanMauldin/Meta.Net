using System.Collections.Generic;

namespace Meta.Net.Generics.Interfaces
{
    public interface INode<T> where T : class
    {
        Node<T> Parent { get; set; }
        NodeCollection<T> Children { get; }
        T Data { get; }
        Node<T> Root { get; }
        bool IsAncestorOf(Node<T> node);
        bool IsDescendantOf(Node<T> node);
        bool IsWithinHierarchyOf(Node<T> node);
        IEnumerator<T> GetDepthFirstEnumerator();
        IEnumerator<T> GetBreadthFirstEnumerator();
    }
}
