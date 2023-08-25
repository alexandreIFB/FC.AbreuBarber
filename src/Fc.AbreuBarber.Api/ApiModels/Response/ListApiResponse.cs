using System.Xml.Linq;

namespace Fc.AbreuBarber.Api.ApiModels.Response
{
    public class ListApiResponse<TData> : ApiResponse<TData>
    {
        public ListApiResponse(Meta meta, TData data) : base(data)
        {
            Meta = meta;
        }

        public Meta Meta { get; private set; }
    }

    public class Meta
    {
        public Meta(int page, int perPage, int total, int lastPage)
        {
            Page = page;
            PerPage = perPage;
            Total = total;
            LastPage = lastPage;
        }

        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int LastPage { get; set; }
    }
}
