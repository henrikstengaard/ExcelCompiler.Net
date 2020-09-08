using System.Linq;
using System.Reflection;
using ExcelCompiler.Net.Compilers.CSharp;
using ExcelCompiler.Net.Contracts;
using ExcelCompiler.Net.Entities;
using ExcelCompiler.Net.Entities.Tokens;
using Xunit;

namespace ExcelCompiler.Net.Compilers.Tests
{
    public class GivenSimpleWorkbook
    {
        private const string SheetName = "Test";
        private readonly Workbook workbook = new Workbook(new[]
        {
            new Sheet(SheetName, new[]
            {
                new Row(new[]
                {
                    new Cell(
                        "A1",
                        new Formula("A2 * A3",
                            new IFormulaToken[] {new RefToken("A2"), new RefToken("A3"), new MultiplyToken(2)}),
                        CellType.Formula, 0,
                        false, string.Empty),
                    new Cell(
                        "A2", new Formula(string.Empty, Enumerable.Empty<IFormulaToken>()), CellType.Numeric, 42,
                        false, string.Empty),
                    new Cell(
                        "A3", new Formula(string.Empty, Enumerable.Empty<IFormulaToken>()), CellType.Numeric, 2,
                        false, string.Empty)
                })
            })
        });
        
        [Fact]
        public void ShouldReturnModuleCodeWhenBuild()
        {
            var sheetBuilder = new SheetBuilder();
            var workbookBuilder = new WorkbookBuilder(sheetBuilder);
            var moduleBuilder = new AssemblyCodeBuilder(workbookBuilder);

            var cSharpModule = moduleBuilder.Build("UnitTest", workbook);
            Assert.NotNull(cSharpModule);
        }
        
        [Fact]
        [Trait("Category", "Integration")]
        public void ShouldReturnAssemblyWhenCompiled()
        {
            var sheetBuilder = new SheetBuilder();
            var workbookBuilder = new WorkbookBuilder(sheetBuilder);
            var assemblyCodeBuilder = new AssemblyCodeBuilder(workbookBuilder);

            const string assemblyName = "UnitTest";
            var assemblyCode = assemblyCodeBuilder.Build(assemblyName, workbook);
            var assemblyBytes = CSharpCodeCompiler.GetAssemblyBytes(assemblyName, assemblyCode).ToArray();
            Assert.NotEmpty(assemblyBytes);
            
            var assembly = Assembly.Load(assemblyBytes);
            var compiledWorkbook = assembly.CreateInstance($"{assemblyName}.Workbook") as IWorkbook;
            Assert.NotNull(compiledWorkbook);

            var sheet = compiledWorkbook.GetSheet(SheetName);
            sheet.Evaluate();
            var value = sheet.GetNumeric("A1");
            Assert.Equal(84, value);
        }
    }
}