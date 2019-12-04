using System;
using System.Collections.Generic;
using ExcelCompiler.Net.Entities.Tokens;

namespace ExcelCompiler.Net.Entities
{
    public class Formula
    {
        public readonly string Raw;
        public readonly IEnumerable<IFormulaToken> Tokens;

        public Formula(string raw, IEnumerable<IFormulaToken> tokens)
        {
            Raw = raw ?? throw new ArgumentNullException(nameof(raw));
            Tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
        }
    }
}