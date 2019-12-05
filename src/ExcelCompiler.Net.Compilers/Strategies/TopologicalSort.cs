using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelCompiler.Net.Compilers.Strategies
{
    public static class Algorithms
    {
        public static Graph<T> CreateGraphFromVertices<T>(IEnumerable<T> vertices) =>
            new Graph<T>(
                vertices.ToDictionary(key => key, value => new HashSet<T>()),
                Enumerable.Empty<T>());

        public static IEnumerable<T> TopologicalSort<T>(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
            where T : IEquatable<T>
        {
            var iEnumerable = vertices.ToList();

            var graph = CreateGraphFromVertices(iEnumerable)
                .AddEdges(edges)
                .FindStartVertices();
                //.AddAdjacentNeighbours();

            var incomingEdges = edges.Select(edge => edge.Item1).Distinct()
                .ToDictionary(key => key, value => new HashSet<T>());
                
            var dependencyEdges = edges.Select(edge => edge.Item2).Distinct()
                .ToDictionary(key => key, value => new HashSet<T>());
            
            foreach (var edge in edges)
            {
                if (incomingEdges[edge.Item1].Contains(edge.Item2))
                {
                    continue;
                }
                incomingEdges[edge.Item1].Add(edge.Item2);
                
                if (dependencyEdges[edge.Item2].Contains(edge.Item1))
                {
                    continue;
                }
                dependencyEdges[edge.Item2].Add(edge.Item1);
            }
                
            foreach (var vertex in iEnumerable.Where(vertex =>
                !incomingEdges.ContainsKey(vertex) && !dependencyEdges.ContainsKey(vertex)))
            {
                yield return vertex;
            }

            var startNodes = iEnumerable.Where(vertex => dependencyEdges.ContainsKey(vertex)).ToList();
            var nodes = startNodes.ToDictionary(key => key, value => new Node<T>(value));
            
            var stack = new Stack<T>(startNodes);

            var dependencies = dependencyEdges.Select(x =>
                    new Tuple<T, HashSet<Node<T>>>(x.Key,
                        new HashSet<Node<T>>(x.Value.Select(n => new Node<T>(n)))))
                .ToDictionary(x => x.Item1, x => x.Item2);

            while (stack.Count > 0)
            {
                var vertex = stack.Pop();

                if (nodes[vertex].Visited)
                    continue;

                nodes[vertex].Visited = true;

                if (dependencies.ContainsKey(vertex))
                {
                    foreach (var neighbor in dependencies[vertex].Where(neighbor => !nodes[neighbor.Value].Visited))
                    {
                        neighbor.Visited = true;
                        stack.Push(neighbor.Value);
                    }
                }

                yield return vertex;
            }

            var firstCircularDependency =
                dependencies.FirstOrDefault(dependency => !dependency.Value.Any(node => node.Visited));
            if (firstCircularDependency.Equals(new KeyValuePair<T, HashSet<Node<T>>>()))
            {
                yield break;
            }
            
            var firstNode = firstCircularDependency.Value.FirstOrDefault(node => !node.Visited);
            var nodeValue = firstNode != null && firstNode.Value != null ? firstNode.Value.ToString() : string.Empty;
            throw new CircularException(
                $"Circular dependency from '{firstCircularDependency.Key}' to '{nodeValue}'");
        }
    }

    public class CircularException : Exception
    {
        public CircularException(string message) : base(message)
        {
        }
    }
}