namespace Bike360.UI.Services.Base;

public class Response<T>
{
    public string Message { get; set; }
    public string VadlidationErrors { get; set; }
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
}
