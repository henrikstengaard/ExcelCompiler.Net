﻿using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Entities.Comparable.Values;
using Xunit;

namespace ExcelCompiler.Net.Tests
{
    public class GivenTwoNumericValues
    {
        private readonly ComparableValue comparableValue1 = new ComparableValue(new NumericValue(10));
        private readonly ComparableValue comparableValue2 = new ComparableValue(new NumericValue(2));

        [Fact]
        public void ShouldReturnNumericValueWhenAdded()
        {
            var result = comparableValue1 + comparableValue2;
            var numericValue = result.Value as NumericValue;
            Assert.NotNull(numericValue);
            Assert.Equal(12.0d, numericValue.Value);
        }

        [Fact]
        public void ShouldReturnNumericValueWhenSubtracted()
        {
            var result = comparableValue1 - comparableValue2;
            var numericValue = result.Value as NumericValue;
            Assert.NotNull(numericValue);
            Assert.Equal(8.0d, numericValue.Value);
        }

        [Fact]
        public void ShouldReturnNumericValueWhenMultiplied()
        {
            var result = comparableValue1 * comparableValue2;
            var numericValue = result.Value as NumericValue;
            Assert.NotNull(numericValue);
            Assert.Equal(20.0d, numericValue.Value);
        }

        [Fact]
        public void ShouldReturnNumericValueWhenDivided()
        {
            var result = comparableValue1 / comparableValue2;
            var numericValue = result.Value as NumericValue;
            Assert.NotNull(numericValue);
            Assert.Equal(5.0d, numericValue.Value);
        }
    }
}