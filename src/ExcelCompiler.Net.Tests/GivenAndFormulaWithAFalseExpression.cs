using Xunit;
using static ExcelCompiler.Net.Formulas.Formula;

namespace ExcelCompiler.Net.Tests
{
    public class GivenAndFormulaWithAFalseExpression
    {
        [Fact]
        public void ShouldReturnFalseWithOneFalseExpression()
        {
            var result = And(false, true);
            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnFalseWithTwoFalseExpressions()
        {
            var result = And(false, false);
            Assert.False(result);
        }
        
        [Fact]
        public void ShouldReturnFalseWithOneComplexFalseExpression()
        {
            const int value1 = 1;
            var result = And(value1 == 2, true);
            Assert.False(result);
        }
        
        [Fact]
        public void ShouldReturnFalseWithTwoComplexFalseExpressions()
        {
            const int value1 = 1;
            const string value2 = "TEST1";
            var result = And(value1 == 2, value2 == "TEST2");
            Assert.False(result);
        }
    }
}