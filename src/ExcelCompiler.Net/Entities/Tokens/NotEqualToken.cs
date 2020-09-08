namespace ExcelCompiler.Net.Entities.Tokens
{
    using System;

    public class NotEqualToken : IFormulaToken
    {
        public readonly int Operands;

        public NotEqualToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}