using System.Collections.Generic;

namespace ExcelCompiler.Net.Contracts
{
    public interface IWorkbook
    {
        ISheet GetSheet(int index);
        ISheet GetSheet(string name);
        IEnumerable<ISheet> GetSheets();
    }
}