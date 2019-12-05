namespace ExcelCompiler.Net.Contracts
{
    public interface ISheet
    {
        string Name { get; }
        string GetString(string cellReference);
        double GetNumeric(string cellReference);
        void SetString(string cellReference, string value);
        void SetNumeric(string cellReference, double value);
        void Evaluate();
    }
}