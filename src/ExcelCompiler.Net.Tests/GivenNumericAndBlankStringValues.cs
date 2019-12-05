using System;
using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Comparable.Values;
using Xunit;

namespace ExcelCompiler.Net.Tests
{
    public class GivenNumericAndBlankStringValues
    {
        private readonly ComparableValue comparableValue1 = new ComparableValue(new NumericValue(1));
        private readonly ComparableValue comparableValue2 = new ComparableValue(new StringValue(string.Empty));
        
        [Fact]
        public void ShouldReturnFalseWhenComparedEqual()
        {
            var result = comparableValue1 == comparableValue2;
            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnTrueWhenComparedGreaterThan()
        {
            var result = comparableValue1 > comparableValue2;
            Assert.True(result);
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
            Assert.False(result);
        }
        
        [Fact]
        public void ShouldReturnNumericValueWhenAdded()
        {
            var result = comparableValue1 + comparableValue2;
            var numericValue = result.Value as NumericValue;
            Assert.NotNull(numericValue);
            Assert.Equal(1, numericValue.Value);
        }

        [Fact]
        public void ShouldReturnNumericValueWhenSubtracted()
        {
            var result = comparableValue1 - comparableValue2;
            var numericValue = result.Value as NumericValue;
            Assert.NotNull(numericValue);
            Assert.Equal(1, numericValue.Value);
        }

        [Fact]
        public void ShouldReturnNumericValueWhenMultiplied()
        {
            var result = comparableValue1 * comparableValue2;
            var numericValue = result.Value as NumericValue;
            Assert.NotNull(numericValue);
            Assert.Equal(0, numericValue.Value);
        }
        
        [Fact]
        public void ShouldReturnFailWhenDivided()
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                var unused = comparableValue1 / comparableValue2;
            });
        }
    }
}