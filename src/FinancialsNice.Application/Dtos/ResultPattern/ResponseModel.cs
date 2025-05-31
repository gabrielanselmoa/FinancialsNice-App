namespace FinancialsNice.Application.Dtos.ResultPattern;

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