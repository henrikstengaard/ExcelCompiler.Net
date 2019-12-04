using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class SubtractToken : IFormulaToken
    {
        public readonly int Operands;

        public SubtractToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}