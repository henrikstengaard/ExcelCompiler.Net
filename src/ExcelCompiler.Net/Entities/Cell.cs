using System;
using System.ComponentModel;

namespace ExcelCompiler.Net.Entities
{
    public class Cell
    {
        public readonly string Reference;
        public readonly Formula Formula;
        public readonly CellType Type;
        public readonly double NumericValue;
        public readonly bool BooleanValue;
        public readonly string StringValue;

        public Cell(string reference, Formula formula, CellType type, double numericValue, bool booleanValue,
            string stringValue)
        {
            Reference = reference ?? throw new ArgumentNullException(nameof(reference));
            Formula = formula ?? throw new ArgumentNullException(nameof(formula));
            if (!Enum.IsDefined(typeof(CellType), type))
                throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(CellType));
            Type = type;
            NumericValue = numericValue;
            BooleanValue = booleanValue;
            StringValue = stringValue ?? throw new ArgumentNullException(nameof(stringValue));
        }
    }
}