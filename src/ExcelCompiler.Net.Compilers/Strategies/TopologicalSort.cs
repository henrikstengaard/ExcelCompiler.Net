using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelCompiler.Net.Compilers.Strategies
{
    public class TopologicalSort<T>
    {
        private class Relations
        {
            public int Dependencies;
            public readonly HashSet<T> Dependents = new HashSet<T>();
        }
        
        private readonly Dictionary<T, Relations> map = new Dictionary<T, Relations>();

        public void Add(T obj)
        {
            if (!map.ContainsKey(obj)) map.Add(obj, new Relations());
        }
        
        public void Add(T obj, T dependency)
        {
            if (dependency.Equals(obj)) return;

            if (!map.ContainsKey(dependency)) map.Add(dependency, new Relations());

            var dependents = map[dependency].Dependents;

            if (!dependents.Contains(obj))
            {
                dependents.Add(obj);

                if (!map.ContainsKey(obj)) map.Add(obj, new Relations());

                ++map[obj].Dependencies;
            }
        }
        public void Add(T obj, IEnumerable<T> dependencies)
        {
            foreach (var dependency in dependencies) Add(obj, dependency);
        }

        public void Add(T obj, params T[] dependencies)
        {
            Add(obj, dependencies as IEnumerable<T>);
        }

        public Tuple<IEnumerable<T>, IEnumerable<T>> Sort()
        {
            List<T> sorted = new List<T>(), cycled = new List<T>();
            var map = this.map.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            sorted.AddRange(map.Where(kvp => kvp.Value.Dependencies == 0).Select(kvp => kvp.Key));

            for (var idx = 0; idx < sorted.Count; ++idx) sorted.AddRange(map[sorted[idx]].Dependents.Where(k => --map[k].Dependencies == 0));

            cycled.AddRange(map.Where(kvp => kvp.Value.Dependencies != 0).Select(kvp => kvp.Key));

            return new Tuple<IEnumerable<T>, IEnumerable<T>>(sorted, cycled);
        }

        public void Clear()
        {
            map.Clear();
        }        
    }
}