using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelCompiler.Net.Compilers.Strategies
{
    public class Graph<T>
    {
        public readonly IReadOnlyDictionary<T, HashSet<T>> AdjacencyList;  
        public readonly IEnumerable<T> StartVertices;  
        
        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T,T>> edges)
        {
            AdjacencyList = vertices.ToDictionary(key => key, value => new HashSet<T>());

            // should create a new graph with modifications
            foreach (var edge in edges)
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
            }

            // should create a new graph with modifications
            StartVertices = AdjacencyList.Where(x => !x.Value.Any()).Select(x => x.Key).ToList();

            // should create a new graph with modifications
            var items = AdjacencyList.SelectMany(adjacent => adjacent.Value.Select(x => new Tuple<T, T>(x, adjacent.Key)));
            foreach(var (vertex, neighbor) in items)
            {
                if (AdjacencyList[vertex].Contains(neighbor))
                {
                    continue;
                }
                AdjacencyList[vertex].Add(neighbor);
            }
        }
    }
}