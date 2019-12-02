using System;
using ExcelCompiler.Net.Entities.Comparable.Values;

namespace ExcelCompiler.Net.Extensions
{
    public static class ValueExtensions
    {
        public static string AsString(this IValue value)
        {
            var stringValue = value as StringValue;
            if (stringValue == null)
            {
                throw new InvalidCastException("Value is not a string");
            }
            return stringValue.Value;
        }

        public static bool IsString(this IValue value) => value is StringValue;

        public static bool IsEmptyString(this IValue value) => string.IsNullOrEmpty(value.AsString());
    }
}