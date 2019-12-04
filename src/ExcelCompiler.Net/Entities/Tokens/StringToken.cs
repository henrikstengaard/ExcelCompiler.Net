using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class StringToken : IFormulaToken
    {
        public readonly string Value;

        public StringToken(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}