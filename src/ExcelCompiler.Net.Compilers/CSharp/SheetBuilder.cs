using System.Collections.Generic;
using System.Text;
using ExcelCompiler.Net.Entities;

namespace ExcelCompiler.Net.Compilers.CSharp
{
    public class SheetBuilder
    {
        private readonly CellsBuilder cellsBuilder;
        private readonly FormulasBuilder formulasBuilder;

        public SheetBuilder(CellsBuilder cellsBuilder, FormulasBuilder formulasBuilder)
        {
            this.cellsBuilder = cellsBuilder;
            this.formulasBuilder = formulasBuilder;
        }

        public string Build(string className, Sheet sheet)
        {
            // topological sort cells!
            
            var code = new StringBuilder();
            code.AppendLine($"public class {className} : ISheet");
            code.AppendLine("{");
            code.AppendLine("}");
            return code.ToString();
        }
    }

    public class CellsBuilder
    {
        public string Build(IEnumerable<Cell> cells)
        {
            var code = new StringBuilder();
            return code.ToString();
        }
    }

    public class FormulasBuilder
    {
        public string Build(IEnumerable<Cell> cells)
        {
            var code = new StringBuilder();
            return code.ToString();
        }
    }
}