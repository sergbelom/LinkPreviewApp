namespace LinkPreviewApp.Common.Http;

public class HttpServiceResponse<T>
{
    public T Content { get; set; }
    public object Error { get; set; }
    public int StatusCode { get; set; }
    public bool IsSuccessful { get; set; }

    public HttpServiceResponse(T content, object error, int statusCode, bool isSuccessful)
    {
        Content = content;
        Error = error;
        StatusCode = statusCode;
        IsSuccessful = isSuccessful;
    }
}
