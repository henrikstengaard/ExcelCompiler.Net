using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelCompiler.Net.Entities;
using ExcelCompiler.Net.Entities.Tokens;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using CellType = ExcelCompiler.Net.Entities.CellType;
using Formula = ExcelCompiler.Net.Entities.Formula;

namespace ExcelCompiler.Net.Readers
{
    public class ExcelReader
    {
        private readonly XSSFWorkbook workbook;
        private readonly XSSFEvaluationWorkbook xssfEvaluationWorkbook;

        public ExcelReader(Stream stream)
        {
            workbook = new XSSFWorkbook(stream);
            xssfEvaluationWorkbook = XSSFEvaluationWorkbook.Create(workbook);
        }

        public Workbook ReadWorkbook() => new Workbook(ReadSheets());

        private IEnumerable<Sheet> ReadSheets()
        {
            for (var s = 0; s < workbook.NumberOfSheets; s++)
            {
                yield return ReadSheet(workbook.GetSheetAt(s));
            }
        }

        private Sheet ReadSheet(ISheet sheet) => new Sheet(sheet.SheetName, GetRows(sheet));

        private IEnumerable<Row> GetRows(ISheet sheet)
        {
            for (var r = sheet.FirstRowNum; r <= sheet.LastRowNum; r++)
            {
                var row = sheet.GetRow(r);
                if (row == null)
                {
                    continue;
                }
                yield return new Row(GetCells(row));
            }
        }

        private IEnumerable<Cell> GetCells(IRow row)
        {
            for (var c = row.FirstCellNum; c <= row.LastCellNum; c++)
            {
                var cell = row.GetCell(c);
                if (cell == null)
                {
                    continue;
                }
                yield return GetCell(cell);
            }
        }

        private Cell GetCell(ICell cell) =>
            new Cell(
                GetCellReference(cell),
                GetFormula(cell),
                GetCellType(cell.CellType),
                GetNumericValue(cell),
                GetBooleanValue(cell),
                GetStringValue(cell)
            );

        private string GetCellReference(ICell cell) =>
            new CellReference(cell).FormatAsString();

        private Formula GetFormula(ICell cell)
        {
            if (cell.CellType != NPOI.SS.UserModel.CellType.Formula)
            {
                return new Formula(string.Empty, Enumerable.Empty<IFormulaToken>());
            }
            var formula = cell.CellFormula ?? string.Empty;
            return new Formula(
                formula,
                GetFormulaTokens(formula));
        }

        private IEnumerable<IFormulaToken> GetFormulaTokens(string cellFormula)
        {
            return FormulaParser.Parse(cellFormula, xssfEvaluationWorkbook, FormulaType.NamedRange, 0)
                .Select(GetFormulaToken);
        }

        private IFormulaToken GetFormulaToken(Ptg ptg)
        {
            switch (ptg)
            {
                case AddPtg addPtg:
                    return new AddToken(addPtg.NumberOfOperands);
                case SubtractPtg subtractPtg:
                    return new SubtractToken(subtractPtg.NumberOfOperands);
                case MultiplyPtg multiplyPtg:
                    return new MultiplyToken(multiplyPtg.NumberOfOperands);
                case DividePtg dividePtg:
                    return new DivideToken(dividePtg.NumberOfOperands);
                case GreaterEqualPtg greaterEqualPtg:
                    return new GreaterEqualToken(greaterEqualPtg.NumberOfOperands);
                case GreaterThanPtg greaterThanPtg:
                    return new GreaterThanToken(greaterThanPtg.NumberOfOperands);
                case LessEqualPtg lessEqualPtg:
                    return new LessEqualToken(lessEqualPtg.NumberOfOperands);
                case LessThanPtg lessThanPtg:
                    return new LessThanToken(lessThanPtg.NumberOfOperands);
                case EqualPtg equalPtg:
                    return new EqualToken(equalPtg.NumberOfOperands);
                case IntPtg intPtg:
                    return new IntegerToken(intPtg.Value);
                case ParenthesisPtg _:
                    return new ParenthesisToken();
                case AttrPtg attrPtg:
                    if (attrPtg.IsOptimizedIf || attrPtg.IsOptimizedChoose || attrPtg.IsSkip) {
                        return new SkipToken();
                    }
                    if (attrPtg.IsSpace) {
                        return new SkipToken();
                    }
                    if (attrPtg.IsSemiVolatile) {
                        return new SkipToken();
                    }                        
                    if (attrPtg.IsSum)
                    {
                        return new SumToken();
                    }
                    throw new Exception($"Unsupported formula token '{attrPtg}'");
                case AreaPtg areaPtg:
                    var cellRange = CellRangeAddress.ValueOf(areaPtg.ToFormulaString());

                    var cellReferences = new List<string>();
                    for (var row = cellRange.FirstRow; row <= cellRange.LastRow; row++)
                    {
                        for (var col = cellRange.FirstColumn; col <= cellRange.LastColumn; col++)
                        {
                            cellReferences.Add(new CellReference(row, col).FormatAsString());
                        }
                    }
                    return new AreaToken(cellReferences);
                case RefPtg refPtg:
                    return new RefToken(new CellReference(refPtg.Row, refPtg.Column).FormatAsString());
                case FuncVarPtg funcVarPtg:
                    return new FuncVarToken(funcVarPtg.Name, funcVarPtg.NumberOfOperands);
                case FuncPtg funcPtg:
                    return new FuncToken(funcPtg.Name, funcPtg.NumberOfOperands);
                case StringPtg stringPtg:
                    return new StringToken(stringPtg.Value);
                case NumberPtg numberPtg:
                    return new NumberToken(numberPtg.Value);
                default:
                    throw new Exception($"Unsupported formula token '{ptg}'");
            }
        }

        private CellType GetCellType(NPOI.SS.UserModel.CellType cellType)
        {
            if (!Enum.TryParse<CellType>(cellType.ToString(), out var type))
            {
                throw new ArgumentOutOfRangeException();
            }

            return type;
        }

        private double GetNumericValue(ICell cell) =>
            cell.CellType == NPOI.SS.UserModel.CellType.Numeric ? cell.NumericCellValue : 0.0d;

        private bool GetBooleanValue(ICell cell) =>
            cell.CellType == NPOI.SS.UserModel.CellType.Boolean && cell.BooleanCellValue;

        private string GetStringValue(ICell cell) => cell.CellType == NPOI.SS.UserModel.CellType.String
            ? cell.StringCellValue ?? string.Empty
            : string.Empty;
    }
}