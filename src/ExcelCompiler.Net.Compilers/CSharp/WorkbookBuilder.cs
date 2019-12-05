using System;
using System.Linq;
using System.Text;
using ExcelCompiler.Net.Entities;

namespace ExcelCompiler.Net.Compilers.CSharp
{
    public class WorkbookBuilder
    {
        private readonly SheetBuilder sheetBuilder;

        public WorkbookBuilder(SheetBuilder sheetBuilder)
        {
            this.sheetBuilder = sheetBuilder;
        }

        public string Build(Workbook workbook)
        {
            var sheetIndex = 0;
            var sheets = workbook.Sheets.Select(x => new Tuple<string, Sheet>($"Sheet{++sheetIndex}", x)).ToList();

            var code = new StringBuilder();
            foreach (var sheet in sheets)
            {
                code.AppendLine(sheetBuilder.Build(sheet.Item1, sheet.Item2));
            }

            code.AppendLine("public class Workbook : IWorkbook");
            code.AppendLine("{");
            code.AppendLine("\tprivate readonly IList<ISheet> sheets;");
            code.AppendLine("\tprivate readonly IDictionary<string, ISheet> index;");
            code.AppendLine("\tpublic Workbook()");
            code.AppendLine("\t{");
            code.AppendLine("\t\tsheets = new List<ISheet>(new []{");
            code.AppendLine(
                string.Join($",{Environment.NewLine}", sheets.Select(sheet => $"\t\t\tnew {sheet.Item1}()")));
            code.AppendLine("\t\t});");
            code.AppendLine("\t\tindex = sheets.ToDictionary(key => key.Name, value => value, StringComparer.Ordinal);");
            code.AppendLine("\t}");

            code.AppendLine("\tpublic ISheet GetSheet(int index)");
            code.AppendLine("\t{");
            code.AppendLine("\t\tif (index >= sheets.Count)");
            code.AppendLine("\t\t{");
            code.AppendLine("\t\t\tthrow new ArgumentOutOfRangeException(nameof(index));");
            code.AppendLine("\t\t}");
            code.AppendLine("\t\treturn sheets[index];");
            code.AppendLine("\t}");

            code.AppendLine("\tpublic ISheet GetSheet(string name)");
            code.AppendLine("\t{");
            code.AppendLine("\t\tif (!index.ContainsKey(name))");
            code.AppendLine("\t\t{");
            code.AppendLine("\t\t\tthrow new ArgumentOutOfRangeException(nameof(name));");
            code.AppendLine("\t\t}");
            code.AppendLine("\t\treturn index[name];");
            code.AppendLine("\t}");

            code.AppendLine("\tpublic IEnumerable<ISheet> GetSheets()");
            code.AppendLine("\t{");
            code.AppendLine("\t\treturn sheets;");
            code.AppendLine("\t}");

            code.AppendLine("}");
            return code.ToString();
        }
    }
}