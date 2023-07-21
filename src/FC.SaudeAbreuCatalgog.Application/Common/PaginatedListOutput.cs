

namespace FC.AbreuBarber.Application.Common
{
    public abstract class PaginatedListOutput<TOutputItem>
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int LastPage { get; set; }
        public IReadOnlyList<TOutputItem> Items { get; set; }

        public PaginatedListOutput(
            int page,
            int perPage,
            int total,
            int lastPage,
            IReadOnlyList<TOutputItem> items)
        {
            Page = page;
            PerPage = perPage;
            Total = total;
            LastPage = lastPage;
            Items = items;
        }
    }
}
