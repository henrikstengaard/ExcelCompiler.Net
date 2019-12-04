using System;
using System.Collections.Generic;

namespace ExcelCompiler.Net.Entities.Tokens
{
    public class AreaToken : IFormulaToken
    {
        public readonly IEnumerable<string> CellReferences;

        public AreaToken(IEnumerable<string> cellReferences)
        {
            CellReferences = cellReferences ?? throw new ArgumentNullException(nameof(cellReferences));
        }
    }
}