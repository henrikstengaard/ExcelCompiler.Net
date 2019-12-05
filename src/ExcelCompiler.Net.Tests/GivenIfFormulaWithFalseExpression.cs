using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Comparable.Values;
using static ExcelCompiler.Net.Formulas.Formula;
using Xunit;

namespace ExcelCompiler.Net.Tests
{
    public class GivenIfFormulaWithFalseExpression
    {
        [Fact]
        public void ShouldReturnFalseValue()
        {
            var trueValue = new ComparableValue(new NumericValue(1));
            var falseValue = new ComparableValue(new NumericValue(0));
            var result = If(false, trueValue, falseValue);
            Assert.Equal(falseValue, result);
        }
    }
}