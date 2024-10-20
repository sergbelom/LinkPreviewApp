using Microsoft.Maui.Controls;
//using Security;
using System.Numerics;
using System.Text;
using System.Text.Json;
//using static ObjCRuntime.Dlfcn;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LinkPreviewApp.Common.Http
{
    public class HttpService : IHttpService
    {
        private const string APYKey = "X-API-Key";

        private readonly IHttpClientService _httpClientService;

        //private string _apyKey;

        public HttpService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            //_apyKey = ;
        }

        public async Task<HttpServiceResponse<T>> SendAsync<T>(string baseUrl, string link) where T : class
        {
            //var httpClient = _httpClientService.CreateHttpClient();
            //var serializedContent = JsonSerializer.Serialize(link, JsonSerializerOptions.Default);

            //var stringRequestContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");

            //var request = new HttpRequestMessage();

            // Base URL
                
            // Query parameters
            string fields = "image_x,icon_type,locale";
            string url = $"?fields={fields}&q={Uri.EscapeDataString(link)}";

            HttpResponseMessage httpResponse = null;
            string responseBody = string.Empty;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var apiKey = "d94451fb5a9d9bef0d83f0fb910dded2";
            try
            {
                client.DefaultRequestHeaders.Add("X-Linkpreview-Api-Key", apiKey);
                httpResponse = await client.GetAsync(url);
                httpResponse.EnsureSuccessStatusCode();
                responseBody = await httpResponse.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                // Handle any HTTP request errors
                //Console.WriteLine($"Request error: {e.Message}");
                }
            //}

            //error hadling:
            //400 Generic error
            //401 Cannot verify API access key
            //403 Invalid or blank API access key
            //423 Forbidden by robots.txt - the requested website does not allow us to access this page
            //425 Invalid response status code(with the actual response code we got from the remote server)
            //426 Too many requests per second on a single domain
            //429 Too many requests / rate limit exceeded

            if (httpResponse != null && httpResponse.IsSuccessStatusCode)
            {
                var dataResponse = JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return new HttpServiceResponse<T>(dataResponse, null, (int)httpResponse.StatusCode, true);
            }

            var errorResponse = JsonSerializer.Deserialize<object>(responseBody);

            return new HttpServiceResponse<T>(default, errorResponse, (int)httpResponse.StatusCode, false);
        }
    }
}
