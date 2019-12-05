using System;
using System.Collections.Generic;

namespace ExcelCompiler.Net.Compilers.Strategies
{
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