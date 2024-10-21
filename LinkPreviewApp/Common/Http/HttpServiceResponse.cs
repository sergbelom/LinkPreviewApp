using System.Net;

namespace LinkPreviewApp.Common.Http;

public class HttpServiceResponse<T>
{
    public T Content { get; set; }
    public string Error { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccessful { get; set; }

    public HttpServiceResponse(T content, string error, HttpStatusCode statusCode, bool isSuccessful)
    {
        Content = content;
        Error = error;
        StatusCode = statusCode;
        IsSuccessful = isSuccessful;
    }
}
