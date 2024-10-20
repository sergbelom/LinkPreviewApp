namespace LinkPreviewApp.Common.Http;

public interface IHttpService
{
    Task<HttpServiceResponse<T>> SendAsync<T>(string baseUrl, string link) where T : class;
}
