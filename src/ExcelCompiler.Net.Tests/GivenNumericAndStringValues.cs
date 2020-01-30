using System;
using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Comparable.Values;
using Xunit;

namespace ExcelCompiler.Net.Tests
{
    public class GivenStringValuesWithWhitespaces
    {
        private readonly ComparableValue comparableValue1 = new ComparableValue(new StringValue("Test"));
        private readonly ComparableValue comparableValue2 = new ComparableValue(new StringValue("Test "));

        [Fact]
        public void ShouldReturnTrueWhenComparedEqual()
        {
            var result = comparableValue1 == comparableValue2;
            Assert.True(result);
        }
        
    }
    
    public class GivenNumericAndStringValues
    {
        private readonly ComparableValue comparableValue1 = new ComparableValue(new NumericValue(1));
        private readonly ComparableValue comparableValue2 = new ComparableValue(new StringValue("A"));
        
        [Fact]
        public void ShouldReturnFalseWhenComparedEqual()
        {
            var result = comparableValue1 == comparableValue2;
            Assert.False(result);
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
            Assert.False(result);
        }
        
        [Fact]
        public void ShouldReturnTrueWhenComparedLessThan()
        {
            var result = comparableValue1 < comparableValue2;
            Assert.True(result);
        }
        
        [Fact]
        public void ShouldReturnTrueWhenComparedLessEqual()
        {
            var result = comparableValue1 <= comparableValue2;
            Assert.True(result);
        }
        
        [Fact]
        public void ShouldFailWhenAdded()
        {
            Assert.Throws<InvalidCastException>(() =>
            {
                var unused = comparableValue1 + comparableValue2;
            });
        }
        
        [Fact]
        public void ShouldFailWhenSubtracted()
        {
            Assert.Throws<InvalidCastException>(() =>
            {
                var unused = comparableValue1 - comparableValue2;
            });
        }
        
        [Fact]
        public void ShouldFailWhenMultiplied()
        {
            Assert.Throws<InvalidCastException>(() =>
            {
                var unused = comparableValue1 * comparableValue2;
            });
        }
        
        [Fact]
        public void ShouldFailWhenDivided()
        {
            Assert.Throws<InvalidCastException>(() =>
            {
                var unused = comparableValue1 / comparableValue2;
            });
        }
    }
}