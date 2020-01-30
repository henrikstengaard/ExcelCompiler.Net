namespace ExcelCompiler.Net.Comparable.Values
{
    public class StringValue : IValue
    {
        public readonly string Value;

        public StringValue(string value)
        {
            if (value != null) Value = value.Trim();
        }
    }
}