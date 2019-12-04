using System;
using System.Collections.Generic;

namespace ExcelCompiler.Net.Entities
{
    public class Sheet
    {
        public readonly string Name;
        public readonly IEnumerable<Row> Rows;

        public Sheet(string name, IEnumerable<Row> rows)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Rows = rows ?? throw new ArgumentNullException(nameof(rows));
        }
    }
}