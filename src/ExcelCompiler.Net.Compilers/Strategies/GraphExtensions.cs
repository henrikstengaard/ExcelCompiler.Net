using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelCompiler.Net.Compilers.Strategies
{
    public static class GraphExtensions
    {
        public static Graph<T> AddEdges<T>(this Graph<T> graph, IEnumerable<Tuple<T, T>> edges)
        {
            var adjacencyList = graph.AdjacencyList.ToDictionary(key => key.Key, value => value.Value);
            foreach (var edge in edges)
            {
                adjacencyList[edge.Item1].Add(edge.Item2);
            }

            return new Graph<T>(
                adjacencyList,
                graph.StartVertices);
        }

        public static Graph<T> FindStartVertices<T>(this Graph<T> graph) =>
            new Graph<T>(
                graph.AdjacencyList,
                graph.AdjacencyList.Where(x => !x.Value.Any()).Select(x => x.Key).ToList());

        public static Graph<T> AddAdjacentNeighbours<T>(this Graph<T> graph)
        {
            var adjacencyList = graph.AdjacencyList.ToDictionary(key => key.Key, value => value.Value);
            var items = adjacencyList.SelectMany(adjacent =>
                adjacent.Value.Select(x => new Tuple<T, T>(x, adjacent.Key)));
            foreach (var (vertex, neighbor) in items)
            {
                if (adjacencyList[vertex].Contains(neighbor))
                {
                    continue;
                }

                adjacencyList[vertex].Add(neighbor);
            }

            return new Graph<T>(
                adjacencyList,
                graph.StartVertices);
        }
    }
}