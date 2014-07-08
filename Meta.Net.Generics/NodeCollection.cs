using System;
using System.Collections;
using System.Collections.Generic;
using Meta.Net.Generics.Interfaces;

namespace Meta.Net.Generics
{
    public class NodeCollection<T> : INodeCollection<T> where T : class
    {
        public Node<T> Owner { get; private set; }
        public List<Node<T>> List { get; private set; }

        public NodeCollection(Node<T> owner)
        {
            Owner = null;
            if (owner == null) throw new ArgumentNullException("owner");
            Owner = owner;
            List = new List<Node<T>>();
        }

        public void Add(Node<T> node)
        {
            if (Owner.IsWithinHierarchyOf(node))
                throw new InvalidOperationException("Cannot add an ancestor or descendant.");
            List.Add(node);
            node.Parent = Owner;
        }

        public void Remove(Node<T> node)
        {
            List.Remove(node);
            node.Parent = null;
        }

        public bool Contains(Node<T> node)
        {
            return List.Contains(node);
        }

        public void Clear()
        {
            foreach (var node in this) node.Parent = null;
                List.Clear();
        }

        public int Count
        {
            get
            {
                return List.Count;
            }
        }

        public Node<T> this[int index]
        {
            get
            {
                return List[index];
            }
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        // IEnumerable<T> inherits from IEnumerable, therefore this class 
        // must implement both the generic and non-generic versions of 
        // GetEnumerator. In most cases, the non-generic method can 
        // simply call the generic method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
