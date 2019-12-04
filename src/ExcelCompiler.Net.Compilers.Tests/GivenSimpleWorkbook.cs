using System.Linq;
using ExcelCompiler.Net.Compilers.CSharp;
using ExcelCompiler.Net.Entities;
using ExcelCompiler.Net.Entities.Tokens;
using Xunit;

namespace ExcelCompiler.Net.Compilers.Tests
{
    public class GivenSimpleWorkbook
    {
        [Fact]
        public void ShouldReturnModuleCodeWhenBuild()
        {
            var cellsBuilder = new CellsBuilder();
            var formulasBuilder = new FormulasBuilder();
            var sheetBuilder = new SheetBuilder(cellsBuilder, formulasBuilder);
            var workbookBuilder = new WorkbookBuilder(sheetBuilder);
            var moduleBuilder = new ModuleBuilder(workbookBuilder);

            var workbook = new Workbook(new[]
            {
                new Sheet("Test", new[]
                {
                    new Row(new[]
                    {
                        new Cell(
                            "A1", new Formula(string.Empty, Enumerable.Empty<IFormulaToken>()), CellType.Numeric, 42,
                            false, string.Empty)
                    })
                })
            });

            var cSharpModule = moduleBuilder.Build("UnitTest", workbook);
            Assert.NotNull(cSharpModule);
        }
    }
}