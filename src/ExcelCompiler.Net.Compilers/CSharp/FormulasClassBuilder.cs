using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelCompiler.Net.Compilers.Strategies;
using ExcelCompiler.Net.Entities;
using ExcelCompiler.Net.Entities.Tokens;

namespace ExcelCompiler.Net.Compilers.CSharp
{
    public static class FormulasClassBuilder
    {
        public static string Build(IEnumerable<Cell> cells)
        {
            var iEnumerable = cells.ToList();

            var code = new StringBuilder();
            code.AppendLine("private class Formulas");
            code.AppendLine("{");
            code.AppendLine("\tprivate readonly Cells cells;");
            code.AppendLine("\tpublic Formulas(Cells cells)");
            code.AppendLine("\t{");
            code.AppendLine("\t\tthis.cells = cells;");
            code.AppendLine("\t}");

            foreach (var cell in iEnumerable)
            {
                code.AppendLine($"\tpublic void {cell.Reference}()");
                code.AppendLine("\t{");
                code.AppendLine($"\t\t// Formula: {cell.Formula.Raw}");
                code.AppendLine(
                    $"\t\tcells[\"{cell.Reference}\"] = {FormulaCodeBuilder.FormatFormula(cell.Formula)};");
                code.AppendLine("\t}");
            }

            code.AppendLine("\tpublic void Evaluate()");
            code.AppendLine("\t{");
            code.AppendLine("\t\ttry");
            code.AppendLine("\t\t{");

            var calculationChain = GetCalculationChain(iEnumerable);
            var index = new HashSet<string>(iEnumerable.Select(x => x.Reference), StringComparer.Ordinal);
            
            foreach (var cellReference in calculationChain.Where(x => index.Contains(x)))
            {
                code.AppendLine($"\t\t\t{cellReference}();");
            }

            code.AppendLine("\t\t}");
            code.AppendLine("\t\tcatch(Exception e)");
            code.AppendLine("\t\t{");
            code.AppendLine("\t\t\tthrow new Exception(\"Failed to evaluate formulas\", e);");
            code.AppendLine("\t\t}");
            code.AppendLine("\t}");
            code.AppendLine("}");

            return code.ToString();
        }

        private static IEnumerable<string> GetCalculationChain(IEnumerable<Cell> cells)
        {
            var topologicalSorter = new TopologicalSorter<string>();

            foreach (var cell in cells)
            {
                var cellReferences = cell.Formula.Tokens.OfType<RefToken>().Select(x => x.CellReference)
                    .Concat(cell.Formula.Tokens.OfType<AreaToken>().SelectMany(token => token.CellReferences))
                    .ToList();

                if (!cellReferences.Any())
                {
                    continue;
                }

                topologicalSorter.Add(cell.Reference, cellReferences);
            }

            var (sorted, cycled) = topologicalSorter.Sort();

            if (cycled.Any())
            {
                throw new Exception("Cyclic dependencies");
            }

            return sorted;
        }
    }
}