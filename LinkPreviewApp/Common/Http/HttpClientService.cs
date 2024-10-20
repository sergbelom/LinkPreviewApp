using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkPreviewApp.Common.Http
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClient CreateHttpClient(int timeOut = 120)
        {
            var clientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };

            var httpClient = new HttpClient(clientHandler);

            //autorization header

            httpClient.Timeout = TimeSpan.FromSeconds(timeOut);

            return httpClient;
        }
    }
}
