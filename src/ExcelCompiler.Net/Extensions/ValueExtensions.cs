using System;
using System.Globalization;
using ExcelCompiler.Net.Comparable.Values;

namespace ExcelCompiler.Net.Extensions
{
    public static class ValueExtensions
    {
        public static double AsNumeric(this IValue value)
        {
            switch (value)
            {
                case StringValue stringValue:
                    if (!double.TryParse(stringValue.Value, out var numeric))
                    {
                        return 0;
                    }
                    return numeric;
                case NumericValue numericValue:
                    return numericValue.Value;
                default:
                    return 0;
            }
        }

        public static string AsString(this IValue value)
        {
            switch (value)
            {
                case StringValue stringValue:
                    return stringValue.Value;
                case NumericValue numericValue:
                    return numericValue.Value.ToString(CultureInfo.CurrentCulture);
                default:
                    return String.Empty;
            }
        }
        
        public static bool IsString(this IValue value) => value is StringValue;

        public static bool IsEmptyString(this IValue value) => string.IsNullOrEmpty(value.AsString());
    }
}