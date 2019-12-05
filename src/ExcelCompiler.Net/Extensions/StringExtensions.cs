namespace ExcelCompiler.Net.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string value)
        {
            return value.Length == 0 ? value : string.Concat(value[0].ToString().ToUpper(), value.Substring(1));
        }
    }
}