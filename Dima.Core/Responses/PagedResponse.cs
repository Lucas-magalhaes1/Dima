using System.Drawing;
using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class PagedResponse<TData> : Response<TData>
{
    [JsonConstructor]
    public PagedResponse(TData? data, int totalCount, int currentPage, int pageSize) :base(data)
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage = 1; 
        PageSize = pageSize = Configuration.PageSize;
    }

    public PagedResponse(TData? data, int code = Configuration.StatusCode, string? message = null) : base(data, code,
        message)
    {
        
    }
    
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize); 
    public int PageSize { get; set; } = Configuration.PageSize;
    public int TotalCount { get; set; }
}