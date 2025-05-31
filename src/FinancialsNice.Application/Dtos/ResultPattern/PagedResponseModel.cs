namespace FinancialsNice.Application.Dtos.ResultPattern;

public class PagedResponseModel<T>
{
    public T? Data { get; set; }
    public MetaData Meta { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool Success { get; set; }
}

public class MetaData
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public bool? NextPage { get; set; }
    public bool? PrevPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
