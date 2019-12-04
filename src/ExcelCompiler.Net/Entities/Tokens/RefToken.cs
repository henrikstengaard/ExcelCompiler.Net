using System;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class RefToken : IFormulaToken
    {
        public readonly string CellReference;

        public RefToken(string cellReference)
        {
            CellReference = cellReference ?? throw new ArgumentNullException(nameof(cellReference));
        }
    }
}