using System;
using ExcelCompiler.Net.Compilers.Strategies;
using Xunit;

namespace ExcelCompiler.Net.Compilers.Tests
{
    public class GivenObjectsWithDependencies
    {
        [Fact]
        public void ShouldBeSortedByDependenciesWhenTopologicallySorted()
        {
            var packageA = new Package("A");
            var packageB = new Package("B");
            var packageC = new Package("C");

            var vertices = new[]
            {
                packageA,
                packageB,
                packageC
            };
            
            var edges = new []
            {
                new Tuple<Package, Package>(packageA, packageC),
            };

            var sorted = Algorithms.TopologicalSort(vertices, edges);
            
            Assert.Equal(new []{ packageC, packageA, packageB }, sorted);
        }
    }
}