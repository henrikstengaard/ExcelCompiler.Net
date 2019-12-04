using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class FuncVarToken : IFormulaToken
    {
        public readonly string Name;
        public readonly int Operands;

        public FuncVarToken(string name, int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Operands = operands;
        }
    }
}