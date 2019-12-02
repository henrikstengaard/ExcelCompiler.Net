namespace ExcelCompiler.Net.Entities.Comparable.Values
{
    public class StringValue : IValue
    {
        public readonly string Value;

        public StringValue(string value)
        {
            Value = value;
        }
    }
}