using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class IntegerToken : IFormulaToken
    {
        public readonly int Value;

        public IntegerToken(int value)
        {
            Value = value;
        }
    }
}