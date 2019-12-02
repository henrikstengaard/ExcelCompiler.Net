using static ExcelCompiler.Net.Formulas.Formula;
using Xunit;

namespace ExcelCompiler.Net.Tests
{
    public class GivenAndFormulaWithTrueExpressions
    {
        [Fact]
        public void ShouldReturnTrueWithSimpleTrueExpressions()
        {
            var result = And(true, true);
            Assert.True(result);
        }
        
        [Fact]
        public void ShouldReturnTrueWithComplexTrueExpressions()
        {
            const int value1 = 1;
            const string value2 = "TEST";
            var result = And(value1 == 1, value2 == "TEST");
            Assert.True(result);
        }
    }
}