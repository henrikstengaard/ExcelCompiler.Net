using System.Collections.Generic;

namespace ExcelCompiler.Net.Entities
{
    public class Workbook
    {
        public readonly IEnumerable<Sheet> Sheets;

        public Workbook(IEnumerable<Sheet> sheets)
        {
            Sheets = sheets;
        }
    }
}