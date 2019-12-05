using System;
using System.Linq;
using ExcelCompiler.Net.Compilers.Strategies;
using Xunit;

namespace ExcelCompiler.Net.Compilers.Tests
{
    public class GivenGraphWithCircularEdges
    {
        [Fact]
        public void ShouldFailWhenTopologicallySorted()
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
                new Tuple<Package, Package>(packageA, packageB),
                new Tuple<Package, Package>(packageB, packageA)
            };
            
            Assert.Throws<CircularException>(() => Algorithms.TopologicalSort(vertices, edges).ToList());
        }
    }
}