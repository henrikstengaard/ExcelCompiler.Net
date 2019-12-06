using System;
using System.Globalization;
using ExcelCompiler.Net.Comparable.Values;
using ExcelCompiler.Net.Extensions;

namespace ExcelCompiler.Net.Comparable
{
    public class ComparableValue : IComparable<ComparableValue>
    {
        public readonly IValue Value;

        public ComparableValue(IValue value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private static double GetNumericValue(ComparableValue comparableValue) =>
            comparableValue.Value is NumericValue numericValue ? numericValue.Value : 0;

        private static string GetStringValue(ComparableValue comparableValue) =>
            comparableValue.Value is StringValue stringValue ? stringValue.Value : string.Empty;

        public static ComparableValue operator +(ComparableValue c1, ComparableValue c2)
        {
            if ((c1.Value.IsString() && !c1.Value.IsEmptyString()) ||
                (c2.Value.IsString() && !c2.Value.IsEmptyString()))
            {
                throw new InvalidCastException("Can't add string values");
            }

            return new ComparableValue(new NumericValue(GetNumericValue(c1) + GetNumericValue(c2)));
        }

        public static ComparableValue operator -(ComparableValue c1, ComparableValue c2)
        {
            if ((c1.Value.IsString() && !c1.Value.IsEmptyString()) ||
                (c2.Value.IsString() && !c2.Value.IsEmptyString()))
            {
                throw new InvalidCastException("Can't subtract string values");
            }

            return new ComparableValue(new NumericValue(GetNumericValue(c1) - GetNumericValue(c2)));
        }

        public static ComparableValue operator *(ComparableValue c1, ComparableValue c2)
        {
            if ((c1.Value.IsString() && !c1.Value.IsEmptyString()) ||
                (c2.Value.IsString() && !c2.Value.IsEmptyString()))
            {
                throw new InvalidCastException("Can't multiply string values");
            }

            return new ComparableValue(new NumericValue(GetNumericValue(c1) * GetNumericValue(c2)));
        }

        public static ComparableValue operator /(ComparableValue c1, ComparableValue c2)
        {
            if ((c1.Value.IsString() && !c1.Value.IsEmptyString()) ||
                (c2.Value.IsString() && !c2.Value.IsEmptyString()))
            {
                throw new InvalidCastException("Can't multiply string values");
            }

            var v2 = GetNumericValue(c2);
            return v2 == 0
                ? new ComparableValue(new NumericValue(0))
                : new ComparableValue(new NumericValue(GetNumericValue(c1) / v2));
        }

        public static bool operator ==(ComparableValue comparableValue1, ComparableValue comparableValue2) =>
            !ReferenceEquals(comparableValue1, null) && !ReferenceEquals(comparableValue2, null) &&
            comparableValue1.CompareTo(comparableValue2) == 0;

        public static bool operator !=(ComparableValue comparableValue1, ComparableValue comparableValue2) =>
            !ReferenceEquals(comparableValue1, null) && !ReferenceEquals(comparableValue2, null) &&
            comparableValue1.CompareTo(comparableValue2) != 0;

        public override bool Equals(object obj) => obj is ComparableValue other && Value == other.Value;

        public static bool operator >(ComparableValue comparableValue1, ComparableValue comparableValue2)
        {
            switch (comparableValue1.Value)
            {
                case StringValue stringValue1 when comparableValue2.Value is StringValue stringValue2:
                    return String.Compare(stringValue1.Value, stringValue2.Value, StringComparison.Ordinal) > 0;
                case StringValue stringValue when comparableValue2.Value is NumericValue numericValue:
                    return String.Compare(stringValue.Value, numericValue.Value.ToString(CultureInfo.InvariantCulture),
                               StringComparison.Ordinal) > 0;
                case NumericValue numericValue when comparableValue2.Value is StringValue stringValue:
                    return String.Compare(numericValue.Value.ToString(CultureInfo.InvariantCulture), stringValue.Value,
                               StringComparison.Ordinal) > 0;
                case NumericValue numericValue1 when comparableValue2.Value is NumericValue numericValue2:
                    return numericValue1.Value.CompareTo(numericValue2.Value) > 0;
                default:
                    return false;
            }
        }

        public static bool operator <(ComparableValue comparableValue1, ComparableValue comparableValue2)
        {
            switch (comparableValue1.Value)
            {
                case StringValue stringValue1 when comparableValue2.Value is StringValue stringValue2:
                    return String.Compare(stringValue1.Value, stringValue2.Value, StringComparison.Ordinal) < 0;
                case StringValue stringValue when comparableValue2.Value is NumericValue numericValue:
                    return String.Compare(stringValue.Value, numericValue.Value.ToString(CultureInfo.InvariantCulture),
                               StringComparison.Ordinal) < 0;
                case NumericValue numericValue when comparableValue2.Value is StringValue stringValue:
                    return String.Compare(numericValue.Value.ToString(CultureInfo.InvariantCulture), stringValue.Value,
                               StringComparison.Ordinal) < 0;
                case NumericValue numericValue1 when comparableValue2.Value is NumericValue numericValue2:
                    return numericValue1.Value.CompareTo(numericValue2.Value) < 0;
                default:
                    return false;
            }
        }

        public static bool operator >=(ComparableValue comparableValue1, ComparableValue comparableValue2)
        {
            switch (comparableValue1.Value)
            {
                case StringValue stringValue1 when comparableValue2.Value is StringValue stringValue2:
                    return String.Compare(stringValue1.Value, stringValue2.Value, StringComparison.Ordinal) >= 0;
                case StringValue stringValue when comparableValue2.Value is NumericValue numericValue:
                    return String.Compare(stringValue.Value, numericValue.Value.ToString(CultureInfo.InvariantCulture),
                               StringComparison.Ordinal) >= 0;
                case NumericValue numericValue when comparableValue2.Value is StringValue stringValue:
                    return String.Compare(numericValue.Value.ToString(CultureInfo.InvariantCulture), stringValue.Value,
                               StringComparison.Ordinal) >= 0;
                case NumericValue numericValue1 when comparableValue2.Value is NumericValue numericValue2:
                    return numericValue1.Value.CompareTo(numericValue2.Value) >= 0;
                default:
                    return false;
            }
        }

        public static bool operator <=(ComparableValue comparableValue1, ComparableValue comparableValue2)
        {
            switch (comparableValue1.Value)
            {
                case StringValue stringValue1 when comparableValue2.Value is StringValue stringValue2:
                    return String.Compare(stringValue1.Value, stringValue2.Value, StringComparison.Ordinal) <= 0;
                case StringValue stringValue when comparableValue2.Value is NumericValue numericValue:
                    return String.Compare(stringValue.Value, numericValue.Value.ToString(CultureInfo.InvariantCulture),
                               StringComparison.Ordinal) <= 0;
                case NumericValue numericValue when comparableValue2.Value is StringValue stringValue:
                    return String.Compare(numericValue.Value.ToString(CultureInfo.InvariantCulture), stringValue.Value,
                               StringComparison.Ordinal) <= 0;
                case NumericValue numericValue1 when comparableValue2.Value is NumericValue numericValue2:
                    return numericValue1.Value.CompareTo(numericValue2.Value) <= 0;
                default:
                    return false;
            }
        }

        public override int GetHashCode() => Value.GetHashCode();

        public int CompareTo(ComparableValue other)
        {
            if (Value == null)
            {
                return -1;
            }

            if (other.Value == null)
            {
                return 1;
            }

            if (Value.Equals(other.Value))
            {
                return 0;
            }

            switch (Value)
            {
                case NumericValue numericValue1 when other.Value is NumericValue numericValue2:
                    return numericValue1.Value.CompareTo(numericValue2.Value);
                case StringValue stringValue1 when other.Value is StringValue stringValue2:
                    return String.Compare(stringValue1.Value, stringValue2.Value, StringComparison.Ordinal);
                default:
                    return -1;
            }
        }
    }
}