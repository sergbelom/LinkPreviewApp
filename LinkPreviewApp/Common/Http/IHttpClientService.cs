using System;
namespace LinkPreviewApp.Common.Http;

public interface IHttpClientService
{
    HttpClient CreateHttpClient(Uri baseAddress, int timeOut = 120);
}
