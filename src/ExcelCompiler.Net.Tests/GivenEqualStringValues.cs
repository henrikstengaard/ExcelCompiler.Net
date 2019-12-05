using System;
using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Comparable.Values;
using Xunit;

namespace ExcelCompiler.Net.Tests
{
    public class GivenEqualStringValues
    {
        private readonly ComparableValue comparableValue1 = new ComparableValue(new StringValue("A"));
        private readonly ComparableValue comparableValue2 = new ComparableValue(new StringValue("A"));

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