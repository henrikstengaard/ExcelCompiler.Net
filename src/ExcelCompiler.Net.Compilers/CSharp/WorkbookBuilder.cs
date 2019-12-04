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
            code.AppendLine("\tprivate readonly IDictionary<string, ISheet> Index;");
            code.AppendLine("\tpublic Workbook()");
            code.AppendLine("\t{");
            code.AppendLine("\t\tIndex = new Dictionary<string, ISheet>(new []{");

            foreach (var sheet in sheets)
            {
                code.AppendLine($"\t\t\tnew KeyValuePair<string, ISheet>(\"{sheet.Item2.Name}\", new {sheet.Item1}());");
            }

            code.AppendLine("\t\t}, StringComparer.InvariantCultureIgnoreCase);");
            code.AppendLine("\t}");
            code.AppendLine("}");
            return code.ToString();
        }
    }
}