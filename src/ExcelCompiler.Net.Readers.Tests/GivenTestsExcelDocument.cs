using System.IO;
using System.Linq;
using Xunit;

namespace ExcelCompiler.Net.Readers.Tests
{
    public class GivenTestsExcelDocument
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void ShouldReturnWorkbookWhenRead()
        {
            var reader = new ExcelReader(File.OpenRead(@"Data\tests.xlsx"));
            var result = reader.ReadWorkbook();
            Assert.NotNull(result);
            Assert.NotEmpty(result.Sheets);
            var rows = result.Sheets.SelectMany(x => x.Rows).ToList();
            Assert.NotEmpty(rows);
            var cells = rows.SelectMany(x => x.Cells).ToList();
            Assert.NotEmpty(cells);
        }
    }
}