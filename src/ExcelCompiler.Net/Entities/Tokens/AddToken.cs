﻿using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class AddToken : IFormulaToken
    {
        public readonly int Operands;

        public AddToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}