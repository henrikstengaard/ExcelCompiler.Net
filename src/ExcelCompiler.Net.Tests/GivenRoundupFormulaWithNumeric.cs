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
        public void ShouldReturnNumericRoundedUp1()
        {
            var result = Roundup(new ComparableValue(new NumericValue(4.6d)), new ComparableValue(new NumericValue(0)));
            Assert.Equal(5.0d, result.Value.AsNumeric());
        }

        [Fact]
        public void ShouldReturnNumericRoundedUp2()
        {
            var result = Roundup(new ComparableValue(new NumericValue(4.4d)), new ComparableValue(new NumericValue(0)));
            Assert.Equal(5.0d, result.Value.AsNumeric());
        }
    }
    
    public class GivenRoundDownFormulaWithNumeric
    {
        [Fact]
        public void ShouldReturnNumericRoundedDown1()
        {
            var result = Rounddown(new ComparableValue(new NumericValue(4.6d)), new ComparableValue(new NumericValue(0)));
            Assert.Equal(4.0d, result.Value.AsNumeric());
        }

        [Fact]
        public void ShouldReturnNumericRoundedDown2()
        {
            var result = Rounddown(new ComparableValue(new NumericValue(4.4d)), new ComparableValue(new NumericValue(0)));
            Assert.Equal(4.0d, result.Value.AsNumeric());
        }
    }
}