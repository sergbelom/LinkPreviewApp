namespace LinkPreviewApp.Common.Http
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClient CreateHttpClient(Uri baseAddress, int timeOut = 120)
        {
            var clientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(clientHandler);
            httpClient.BaseAddress = baseAddress;
            httpClient.Timeout = TimeSpan.FromSeconds(timeOut);
            return httpClient;
        }
    }
}
