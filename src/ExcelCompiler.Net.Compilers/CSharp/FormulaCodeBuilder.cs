using System;
using System.Collections.Generic;
using System.Linq;
using ExcelCompiler.Net.Entities;
using ExcelCompiler.Net.Entities.Tokens;
using ExcelCompiler.Net.Extensions;

namespace ExcelCompiler.Net.Compilers.CSharp
{
    public static class FormulaCodeBuilder
    {
        public static string FormatFormula(Formula formula)
        {
            var stack = new Stack<string>();
            foreach (var token in formula.Tokens.Where(token => token.GetType() != typeof(SkipToken)))
            {
                switch (token)
                {
                    case AddToken _:
                        var add1 = stack.Pop();
                        var add2 = stack.Pop();
                        stack.Push($"{add2} + {add1}");
                        break;
                    case SubtractToken _:
                        var sub1 = stack.Pop();
                        var sub2 = stack.Pop();
                        stack.Push($"{sub2} - {sub1}");
                        break;
                    case MultiplyToken _:
                        var mul1 = stack.Pop();
                        var mul2 = stack.Pop();
                        stack.Push($"{mul2} * {mul1}");
                        break;
                    case DivideToken _:
                        var div1 = stack.Pop();
                        var div2 = stack.Pop();
                        stack.Push($"{div2} / {div1}");
                        break;
                    case EqualToken _:
                        var eq1 = stack.Pop();
                        var eq2 = stack.Pop();
                        stack.Push($"{eq2} == {eq1}");
                        break;
                    case GreaterEqualToken _:
                        var ge1 = stack.Pop();
                        var ge2 = stack.Pop();
                        stack.Push($"{ge2} >= {ge1}");
                        break;
                    case GreaterThanToken _:
                        var gt1 = stack.Pop();
                        var gt2 = stack.Pop();
                        stack.Push($"{gt2} > {gt1}");
                        break;
                    case LessEqualToken _:
                        var le1 = stack.Pop();
                        var le2 = stack.Pop();
                        stack.Push($"{le2} <= {le1}");
                        break;
                    case LessThanToken _:
                        var lt1 = stack.Pop();
                        var lt2 = stack.Pop();
                        stack.Push($"{lt2} < {lt1}");
                        break;
                    case ParenthesisToken _:
                        stack.Push($"({stack.Pop()})");
                        break;
                    case RefToken refToken:
                        stack.Push($"cells[\"{refToken.CellReference}\"]");
                        break;
                    case IntegerToken integerToken:
                        stack.Push(CellsClassBuilder.NewComparableValue(CellType.Numeric, string.Empty, integerToken.Value));
                        break;
                    case StringToken stringToken:
                        stack.Push(CellsClassBuilder.NewComparableValue(CellType.String, stringToken.Value, 0));
                        break;
                    case FuncVarToken funcVarToken:
                        stack.Push($"{funcVarToken.Name.ToLower().Capitalize()}({string.Join(", ", GetOperands(stack, funcVarToken.Operands))})");
                        break;
                    case FuncToken funcToken:
                        stack.Push($"{funcToken.Name.ToLower().Capitalize()}({string.Join(", ", GetOperands(stack, funcToken.Operands))})");
                        break;
                    case AreaToken areaToken:
                        stack.Push(string.Join(", ", areaToken.CellReferences.Select(x => $"cells[\"{x}\"]")));
                        break;
                    case SumToken _:
                        stack.Push($"Sum({stack.Pop()})");
                        break;
                    case NumberToken numberToken:
                        stack.Push(CellsClassBuilder.NewComparableValue(CellType.Numeric, string.Empty, numberToken.Value));
                        break;
                    default:
                        throw new Exception($"Token type '{token.GetType()}' is not supported");
                }
            }
            
            return stack.Pop();
        }
        
        public static IEnumerable<string> GetOperands(Stack<string> stack, int numberOfOperands)
        {
            var operands = new List<string>();
            for (var i = 0; i < numberOfOperands; i++)
            {
                operands.Add(stack.Pop());
            }
            operands.Reverse();
            return operands;
        }
    }
}