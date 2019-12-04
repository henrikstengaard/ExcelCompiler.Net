using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class GreaterEqualToken : IFormulaToken
    {
        public readonly int Operands;

        public GreaterEqualToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}