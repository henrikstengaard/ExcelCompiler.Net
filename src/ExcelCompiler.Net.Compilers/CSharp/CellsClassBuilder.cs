using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ExcelCompiler.Net.Entities;
using ExcelCompiler.Net.Entities.Tokens;

namespace ExcelCompiler.Net.Compilers.CSharp
{
    public static class CellsClassBuilder
    {
        private static readonly CultureInfo Culture = new CultureInfo("en");

        public static string Build(IEnumerable<Cell> cells)
        {
            var code = new StringBuilder();
            code.AppendLine("private class Cells");
            code.AppendLine("{");

            code.AppendLine("\tprivate readonly IDictionary<string, ComparableValue> Index;");
            code.AppendLine("\tpublic Cells()");
            code.AppendLine("\t{");
            code.AppendLine("\t\tIndex = new Dictionary<string, ComparableValue>(new []");
            code.AppendLine("\t\t\t{");

            code.AppendLine(
                string.Join($",{Environment.NewLine}", cells.Select(cell =>
                    $"\t\t\t\tnew KeyValuePair<string, ComparableValue>(\"{cell.Reference}\", {NewComparableValue(cell.Type, cell.StringValue, cell.NumericValue)})")));

            code.AppendLine("\t\t\t}, StringComparer.Ordinal);");
            code.AppendLine("\t}");
            code.AppendLine("\tpublic ComparableValue this[string cellReference]");
            code.AppendLine("\t{");
            code.AppendLine("\t\tget");
            code.AppendLine("\t\t{");
            code.AppendLine("\t\t\tif (!Index.ContainsKey(cellReference))");
            code.AppendLine("\t\t\t{");
            code.AppendLine($"\t\t\t\tIndex[cellReference] = {NewComparableValue(CellType.String, string.Empty, 0)};");
            code.AppendLine("\t\t\t}");
            code.AppendLine("\t\t\treturn Index[cellReference];");
            code.AppendLine("\t\t}");
            code.AppendLine("\t\tset");
            code.AppendLine("\t\t{");
            code.AppendLine("\t\t\tif (!Index.ContainsKey(cellReference))");
            code.AppendLine("\t\t\t{");
            code.AppendLine($"\t\t\t\tIndex[cellReference] = {NewComparableValue(CellType.String, string.Empty, 0)};");
            code.AppendLine("\t\t\t}");
            code.AppendLine("\t\t\tIndex[cellReference] = value;");
            code.AppendLine("\t\t}");
            code.AppendLine("\t}");
            code.AppendLine("}");

            return code.ToString();
        }

        public static string NewComparableValue(CellType type, string stringValue, double numericValue)
        {
            switch (type)
            {
                case CellType.String:
                    return $"new ComparableValue(new StringValue(\"{stringValue.Replace("\"", "\\\"")}\"))";
                case CellType.Boolean:
                    throw new NotSupportedException("Boolean cell types are currently not supported");
                case CellType.Numeric:
                    return $"new ComparableValue(new NumericValue({numericValue.ToString(Culture)}))";
                default:
                    return "new ComparableValue(new StringValue(string.Empty))";
            }
        }
    }
}