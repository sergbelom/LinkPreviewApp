namespace LinkPreviewApp.Common.Http
{
    /// <summary>
    /// Client Service for real requests
    /// </summary>
    public class HttpClientService : IHttpClientService
    {
        //possible improvement: use HttpClientFactory

        /// <summary>
        /// Create Http Client use base address
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
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
