namespace ExcelCompiler.Net.Tests
{
    using System.Collections.Generic;
    using Comparable;
    using Comparable.Values;
    using Extensions;
    using Formulas;
    using Xunit;

    public class GivenCountIfFormulaWithListOfValues
    {
        private readonly IEnumerable<ComparableValue> values = new[]
        {
            new ComparableValue(new NumericValue(1)),
            new ComparableValue(new NumericValue(2)),
            new ComparableValue(new NumericValue(3))
        };
        
        [Fact]
        public void ShouldReturnOneIfValueExists()
        {
            var criteriaValue = new ComparableValue(new NumericValue(1));
            var result = Formula.CountIf(values, criteriaValue);
            Assert.Equal(1, result.Value.AsNumeric());
        }
        
        [Fact]
        public void ShouldReturnZeroIfValueExists()
        {
            var criteriaValue = new ComparableValue(new NumericValue(4));
            var result = Formula.CountIf(values, criteriaValue);
            Assert.Equal(0, result.Value.AsNumeric());
        }
    }
}