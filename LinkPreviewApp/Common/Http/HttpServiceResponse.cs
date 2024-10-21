using System.Net;

namespace LinkPreviewApp.Common.Http;

/// <summary>
/// Response container
/// </summary>
/// <typeparam name="T"></typeparam>
public class HttpServiceResponse<T> where T : class
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
