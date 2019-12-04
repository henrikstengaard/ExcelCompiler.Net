﻿using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class LessThanToken : IFormulaToken
    {
        public readonly int Operands;

        public LessThanToken(int operands)
        {
            if (operands <= 0) throw new ArgumentOutOfRangeException(nameof(operands));
            Operands = operands;
        }
    }
}