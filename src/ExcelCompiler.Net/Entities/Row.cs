using System;
using System.Collections.Generic;

namespace ExcelCompiler.Net.Entities
{
    public class Row
    {
        public readonly IEnumerable<Cell> Cells;

        public Row(IEnumerable<Cell> cells)
        {
            Cells = cells ?? throw new ArgumentNullException(nameof(cells));
        }
    }
}