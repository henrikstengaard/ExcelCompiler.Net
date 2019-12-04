namespace ExcelCompiler.Net.Compilers.Tests
{
    public class GivenObjectsWithDependencies
    {
        public void ShouldBeSortedByDependenciesWhenTopologicallySorted()
        {
            //var context = new SortContext();
            var context = new SortContext<Package>();
        }
    }
}