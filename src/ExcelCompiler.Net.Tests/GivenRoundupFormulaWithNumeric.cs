using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Comparable.Values;
using ExcelCompiler.Net.Extensions;
using Xunit;
using static ExcelCompiler.Net.Formulas.Formula;

namespace ExcelCompiler.Net.Tests
{
    public class GivenRoundupFormulaWithNumeric
    {
        [Fact]
        public void ShouldReturnNumericRoundedUp()
        {
            var result = Roundup(new ComparableValue(new NumericValue(4.6d)), new ComparableValue(new NumericValue(0)));
            Assert.Equal(5.0d, result.Value.AsNumeric());
        }

        [Fact]
        public void ShouldReturnNumericRoundedDown()
        {
            var result = Roundup(new ComparableValue(new NumericValue(4.4d)), new ComparableValue(new NumericValue(0)));
            Assert.Equal(5.0d, result.Value.AsNumeric());
        }
    }
}