using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Entities.Comparable.Values;
using ExcelCompiler.Net.Extensions;
using static ExcelCompiler.Net.Formulas.Formula;
using Xunit;

namespace ExcelCompiler.Net.Tests
{
    public class GivenSumFormulaWithListOfValues
    {
        [Fact]
        public void ShouldReturnSumOfValues()
        {
            var comparableValue1 = new ComparableValue(new NumericValue(1));
            var comparableValue2 = new ComparableValue(new NumericValue(2));
            var comparableValue3 = new ComparableValue(new NumericValue(3));
            var result = Sum(comparableValue1, comparableValue2, comparableValue3);
            Assert.Equal(6, result.Value.AsNumeric());
        }
    }
}