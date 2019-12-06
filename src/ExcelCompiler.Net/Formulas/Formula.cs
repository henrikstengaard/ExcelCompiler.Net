using System;
using System.Linq;
using ExcelCompiler.Net.Comparable;
using ExcelCompiler.Net.Comparable.Values;
using ExcelCompiler.Net.Extensions;

namespace ExcelCompiler.Net.Formulas
{
    public static class Formula
    {
        public static ComparableValue If(bool expression, ComparableValue trueValue, ComparableValue falseValue) =>
            expression ? trueValue : falseValue;

        public static bool And(bool expression1, bool expression2) => expression1 && expression2;

        public static bool Or(bool expression1, bool expression2) => expression1 || expression2;

        public static ComparableValue Roundup(ComparableValue value, ComparableValue digits)
        {
            var multiplier = Math.Pow(10, digits.Value.AsNumeric());
            return new ComparableValue(
                new NumericValue(Math.Ceiling(value.Value.AsNumeric() * multiplier / multiplier)));
        }

        public static ComparableValue Sum(params ComparableValue[] values) =>
            values.Aggregate(new ComparableValue(new NumericValue(0)), (current, value) => current + value);
    }
}