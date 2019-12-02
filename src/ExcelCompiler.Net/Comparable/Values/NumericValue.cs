namespace ExcelCompiler.Net.Entities.Comparable.Values
{
    public class NumericValue : IValue
    {
        public readonly double Value;

        public NumericValue(double value)
        {
            Value = value;
        }
    }
}