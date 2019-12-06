namespace ExcelCompiler.Net.Entities.Tokens
{
    public class NumberToken : IFormulaToken
    {
        public readonly double Value;

        public NumberToken(double value)
        {
            Value = value;
        }
    }
}