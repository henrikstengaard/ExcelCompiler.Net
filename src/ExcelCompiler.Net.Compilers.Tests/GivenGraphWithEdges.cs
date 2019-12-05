using System;
using System.Linq;
using ExcelCompiler.Net.Compilers.Strategies;
using Xunit;

namespace ExcelCompiler.Net.Compilers.Tests
{
    public class GivenGraphWithEdges
    {
        [Fact(Skip = "Needs refactoring")]
        public void ShouldReturnSortedVerticesWhenTopologicallySorted()
        {
            var packageA = new Package("A");
            var packageB = new Package("B");
            var packageC = new Package("C");
            var packageD = new Package("D");

            var vertices = new[]
            {
                packageA,
                packageB,
                packageC,
                packageD
            };
            
            var edges = new []
            {
                new Tuple<Package, Package>(packageA, packageC),
                new Tuple<Package, Package>(packageA, packageD)
            };

            var sorted = Algorithms.TopologicalSort(vertices, edges).ToList();
            
            Assert.Equal(new []{ packageB, packageC, packageA }, sorted);
        }
    }
}