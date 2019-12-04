using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class LessEqualToken : IFormulaToken
    {
        public readonly int Operands;

        public LessEqualToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}