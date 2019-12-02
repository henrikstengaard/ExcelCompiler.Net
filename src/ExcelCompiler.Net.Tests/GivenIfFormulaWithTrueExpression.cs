using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Entities.Comparable.Values;
using Xunit;
using static ExcelCompiler.Net.Formulas.Formula;

namespace ExcelCompiler.Net.Tests
{
    public class GivenIfFormulaWithTrueExpression
    {
        [Fact]
        public void ShouldReturnTrueValue()
        {
            var trueValue = new ComparableValue(new NumericValue(1));
            var falseValue = new ComparableValue(new NumericValue(0));
            var result = If(true, trueValue, falseValue);
            Assert.Equal(trueValue, result);
        }
    }
}