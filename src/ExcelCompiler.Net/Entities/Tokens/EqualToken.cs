using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class EqualToken : IFormulaToken
    {
        public readonly int Operands;

        public EqualToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}