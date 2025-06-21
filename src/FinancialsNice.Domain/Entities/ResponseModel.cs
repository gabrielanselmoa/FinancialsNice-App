namespace FinancialsNice.Domain.Design_Pattern;

public class ResponseModel<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
    
    public ResponseModel<T> Fail(T? data, string? message)
        => new() { Data = data, Message = message, Success = false };
    
    public ResponseModel<T> Ok(T data, string? message = null)
        => new() { Data = data, Message = message, Success = true };
}

public class PagedResponseModel<T>
{
    public T? Data { get; set; }
    public MetaData Meta { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool Success { get; set; }
    
    public PagedResponseModel<T> Fail(T? data, MetaData meta, string? message)
        => new() { Data = data, Meta = meta, Message = message, Success = false };

    public PagedResponseModel<T> Ok(T data, MetaData meta, string? message = null)
        => new() { Data = data, Meta = meta, Message = message, Success = true };
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
