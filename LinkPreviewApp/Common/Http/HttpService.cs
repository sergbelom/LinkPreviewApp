using System.Net;
using Newtonsoft.Json;

namespace LinkPreviewApp.Common.Http
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientService _httpClientService;

        public HttpService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<HttpServiceResponse<T>> GetAsync<T>(string baseAddress, string header, string link, Dictionary<string, string> parameters) where T : class
        {
            try
            {
                var baseUri = CreateBaseUri(baseAddress);
                var httpClient = _httpClientService.CreateHttpClient(baseUri);

                var queryString = string.Empty;
                if (parameters != null) {
                    queryString += parameters.ToQueryString();
                }

                var apiKey = "d94451fb5a9d9bef0d83f0fb910dded2";
                httpClient.DefaultRequestHeaders.Add(header, apiKey);

                //TOOD: we need Dispose for the httpClient
                var response = await httpClient.GetAsync(queryString);
                if (response != null)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var evaluatedResponse = JsonConvert.DeserializeObject<T>(responseBody);
                    if (evaluatedResponse != null) {
                        return new HttpServiceResponse<T>(evaluatedResponse, string.Empty, response.StatusCode, response.IsSuccessStatusCode);
                    }
                    else
                    {
                        return new HttpServiceResponse<T>(default(T), "Unable to recognize the response from the service.", HttpStatusCode.InternalServerError, false);
                    }
                }
                else
                {
                    return new HttpServiceResponse<T>(default(T), "No response from service.", HttpStatusCode.InternalServerError, false);
                }
            }
            catch (Exception e)
            {
                return new HttpServiceResponse<T>(default(T), "An internal error occurred.", HttpStatusCode.InternalServerError, false);
                //log e.Message
            }
        }

        private Uri CreateBaseUri(string address)
        {
            return new Uri(Uri.UriSchemeHttps + Uri.SchemeDelimiter + address);
        }
    }
}
