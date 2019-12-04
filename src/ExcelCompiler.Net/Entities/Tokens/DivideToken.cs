using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class DivideToken : IFormulaToken
    {
        public readonly int Operands;

        public DivideToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}