using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkPreviewApp.Common.Http
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClient CreateHttpClient(Uri baseAddress, int timeOut = 120)
        {
            var clientHandler = new HttpClientHandler()
            {
                SslProtocols= System.Security.Authentication.SslProtocols.Tls12
            };

            var httpClient = new HttpClient(clientHandler);
            httpClient.BaseAddress = baseAddress;
            httpClient.Timeout = TimeSpan.FromSeconds(timeOut);
            return httpClient;
        }
    }
}
