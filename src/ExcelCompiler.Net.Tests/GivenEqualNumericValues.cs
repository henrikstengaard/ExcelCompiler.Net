using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Comparable.Values;
using ExcelCompiler.Net.Extensions;
using Xunit;

namespace ExcelCompiler.Net.Tests
{
    public class GivenEqualNumericValues
    {
        private readonly ComparableValue comparableValue1 = new ComparableValue(new NumericValue(5));
        private readonly ComparableValue comparableValue2 = new ComparableValue(new NumericValue(5));

        [Fact]
        public void ShouldReturnTrueWhenComparedEqual()
        {
            var result = comparableValue1 == comparableValue2;
            Assert.True(result);
        }
        
        [Fact]
        public void ShouldReturnFalseWhenComparedGreaterThan()
        {
            var result = comparableValue1 > comparableValue2;
            Assert.False(result);
        }
        
        [Fact]
        public void ShouldReturnTrueWhenComparedGreaterEqual()
        {
            var result = comparableValue1 >= comparableValue2;
            Assert.True(result);
        }
        
        [Fact]
        public void ShouldReturnFalseWhenComparedLessThan()
        {
            var result = comparableValue1 < comparableValue2;
            Assert.False(result);
        }
        
        [Fact]
        public void ShouldReturnTrueWhenComparedLessEqual()
        {
            var result = comparableValue1 <= comparableValue2;
            Assert.True(result);
        }
        
        [Fact]
        public void ShouldReturnNumericValueWhenAdded()
        {
            var result = comparableValue1 + comparableValue2;
            Assert.Equal(10, result.Value.AsNumeric());
        }

        [Fact]
        public void ShouldReturnNumericValueWhenSubtracted()
        {
            var result = comparableValue1 - comparableValue2;
            Assert.Equal(0, result.Value.AsNumeric());
        }

        [Fact]
        public void ShouldReturnNumericValueWhenMultiplied()
        {
            var result = comparableValue1 * comparableValue2;
            Assert.Equal(25, result.Value.AsNumeric());
        }

        [Fact]
        public void ShouldReturnNumericValueWhenDivided()
        {
            var result = comparableValue1 / comparableValue2;
            Assert.Equal(1, result.Value.AsNumeric());
        }
    }
}