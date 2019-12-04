using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class MultiplyToken : IFormulaToken
    {
        public readonly int Operands;

        public MultiplyToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}