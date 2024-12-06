namespace Regulatorio.SharedKernel.Common
{
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }
}