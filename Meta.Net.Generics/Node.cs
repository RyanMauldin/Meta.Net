using System.Collections.Generic;
using System.Linq;
using Meta.Net.Generics.Interfaces;

namespace Meta.Net.Generics
{
    public class Node<T> : INode<T> where T : class
    {
        public Node<T> Parent { get; set; }
        public NodeCollection<T> Children { get; private set; }
        public T Data { get; private set; }

        public Node(T data)
        {
            Children = new NodeCollection<T>(this);
            Data = data;
        }

        public Node<T> Root
        {
            get
            {
                return Parent == null
                    ? this
                    : Parent.Root;
            }
        }

        public bool IsAncestorOf(Node<T> node)
        {
            return Children.Contains(node)
                || Children.Any(p => p.IsAncestorOf(node));
        }

        public bool IsDescendantOf(Node<T> node)
        {
            if (Parent == null) return false;
            return Parent == node || Parent.IsDescendantOf(node);
        }

        public bool IsWithinHierarchyOf(Node<T> node)
        {
            return this == node || IsAncestorOf(node) || IsDescendantOf(node);
        }

        public IEnumerator<T> GetDepthFirstEnumerator()
        {
            yield return Data;
            foreach (var childEnumerator in Children.Select(p => p.GetDepthFirstEnumerator()))
            {
                while (childEnumerator.MoveNext())
                    yield return childEnumerator.Current;
            }
        }

        public IEnumerator<T> GetBreadthFirstEnumerator()
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this);
            while (0 < queue.Count)
            {
                var node = queue.Dequeue();
                foreach (var child in node.Children)
                    queue.Enqueue(child);
                yield return node.Data;
            }
        }
    }
}
