using System;
using System.Collections.Generic;

namespace ExcelCompiler.Net.Compilers.Strategies
{
    public class Node<T> : IEquatable<Node<T>> where T : IEquatable<T>
    {
        public readonly T Value;
        public bool Visited;

        public Node(T value)
        {
            Value = value;
        }

        public bool Equals(Node<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node<T>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }
    
    public class Graph<T>
    {
        public readonly IReadOnlyDictionary<T, HashSet<T>> AdjacencyList;
        public readonly IEnumerable<T> StartVertices;

        public Graph(IReadOnlyDictionary<T, HashSet<T>> adjacencyList, IEnumerable<T> startVertices)
        {
            AdjacencyList = adjacencyList ?? throw new ArgumentNullException(nameof(adjacencyList));
            StartVertices = startVertices ?? throw new ArgumentNullException(nameof(startVertices));
        }
    }
}