using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Entities.Comparable.Values;
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
        public void ShouldReturnFalseWhenComparedGreaterEqual()
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
        public void ShouldReturnFalseWhenComparedLessEqual()
        {
            var result = comparableValue1 <= comparableValue2;
            Assert.True(result);
        }        
    }
}