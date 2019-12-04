using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class GreaterThanToken : IFormulaToken
    {
        public readonly int Operands;

        public GreaterThanToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}