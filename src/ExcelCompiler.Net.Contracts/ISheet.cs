namespace ExcelCompiler.Net.Contracts
{
    public interface ISheet
    {
        string Name { get; }
        T GetValue<T>(string cellReference);
        void SetValue<T>(string cellReference, T value);
        void Evaluate();
    }
}