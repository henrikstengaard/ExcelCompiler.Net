using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelCompiler.Net.Compilers.Strategies
{
    public static class Algorithms
    {
        public static IEnumerable<T> TopologicalSort<T>(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
        {
            var graph = new Graph<T>(vertices, edges);

            var visited = new HashSet<T>();

            if (graph.StartVertices.All(x => !graph.AdjacencyList.ContainsKey(x)))
            {
                throw new ArgumentException("Start vertices doesn´t exist");
            }

            var stack = new Stack<T>(graph.StartVertices);

            while (stack.Count > 0)
            {
                var vertex = stack.Pop();

                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);

                foreach (var neighbor in graph.AdjacencyList[vertex].Where(neighbor => !visited.Contains(neighbor)))
                {
                    stack.Push(neighbor);
                }
            }

            return visited;
        }
    }
}