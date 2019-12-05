using System;
using System.Linq;
using System.Text;
using ExcelCompiler.Net.Entities;

namespace ExcelCompiler.Net.Compilers.CSharp
{
    public class SheetBuilder
    {
        public string Build(string className, Sheet sheet)
        {
            var cells = sheet.Rows.SelectMany(row => row.Cells.Select(cell => cell)).ToList();
            
            var code = new StringBuilder();
            code.AppendLine($"public class {className} : ISheet");
            code.AppendLine("{");
            code.AppendLine(string.Empty);
            code.AppendLine(CellsClassBuilder.Build(cells));
            code.AppendLine(FormulasClassBuilder.Build(cells.Where(cell => cell.Type == CellType.Formula)));
            code.AppendLine(string.Empty);
            code.AppendLine($"\tpublic string Name {{ get {{ return \"{sheet.Name.Replace("\"", "\\\"")}\"; }} }}");
            code.AppendLine("\tprivate readonly Cells cells;");
            code.AppendLine("\tprivate readonly Formulas formulas;");
            code.AppendLine($"\tpublic {className}()");
            code.AppendLine("\t{");
            code.AppendLine("\t\tcells = new Cells();");
            code.AppendLine("\t\tformulas = new Formulas(cells);");
            code.AppendLine("\t}");
            code.AppendLine("\tpublic void Evaluate()");
            code.AppendLine("\t{");
            code.AppendLine("\t\tformulas.Evaluate();");
            code.AppendLine("\t}");

            code.AppendLine("\tpublic string GetString(string cellReference)");
            code.AppendLine("\t{");
            code.AppendLine("\t\treturn cells[cellReference].Value.AsString();");
            code.AppendLine("\t}");

            code.AppendLine("\tpublic double GetNumeric(string cellReference)");
            code.AppendLine("\t{");
            code.AppendLine("\t\treturn cells[cellReference].Value.AsNumeric();");
            code.AppendLine("\t}");
            
            code.AppendLine("\tpublic void SetString(string cellReference, string value)");
            code.AppendLine("\t{");
            code.AppendLine("\t\tcells[cellReference] = new ComparableValue(new StringValue(value));");
            code.AppendLine("\t}");

            code.AppendLine("\tpublic void SetNumeric(string cellReference, double value)");
            code.AppendLine("\t{");
            code.AppendLine("\t\tcells[cellReference] = new ComparableValue(new NumericValue(value));");
            code.AppendLine("\t}");
            code.AppendLine("}");

            return code.ToString();
        }
    }
}