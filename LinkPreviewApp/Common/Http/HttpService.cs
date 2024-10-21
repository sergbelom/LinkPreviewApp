using System.Net;
using LinkPreviewApp.Services;
using Newtonsoft.Json;

namespace LinkPreviewApp.Common.Http
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly AppLogService _logService;

        public HttpService(IHttpClientService httpClientService, AppLogService logService)
        {
            _httpClientService = httpClientService;
            _logService = logService;
        }

        public async Task<HttpServiceResponse<T>> GetAsync<T>(string baseAddress, string header, string apiKey, string link, Dictionary<string, string> parameters) where T : class
        {
            try
            {
                var baseUri = CreateBaseUri(baseAddress);
                var httpClient = _httpClientService.CreateHttpClient(baseUri);

                var queryString = string.Empty;
                if (parameters != null) {
                    queryString += parameters.ToQueryString();
                }

                httpClient.DefaultRequestHeaders.Add(header, apiKey);

                var response = await httpClient.GetAsync(queryString);
                if (response != null)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var evaluatedResponse = JsonConvert.DeserializeObject<T>(responseBody);
                    if (evaluatedResponse != null) {
                        var infoMsg = "Recived evaluated response";
                        _logService.LogWarning(infoMsg);
                        return new HttpServiceResponse<T>(evaluatedResponse, string.Empty, response.StatusCode, response.IsSuccessStatusCode);
                    }
                    else
                    {
                        var warningMsg = "Unable to recognize the response from the service";
                        _logService.LogWarning(warningMsg);
                        return new HttpServiceResponse<T>(default(T), warningMsg, HttpStatusCode.InternalServerError, false);
                    }
                }
                else
                {
                    var errorMsg = "No response from service.";
                    _logService.LogWarning(errorMsg);
                    return new HttpServiceResponse<T>(default(T), errorMsg, HttpStatusCode.InternalServerError, false);
                }
            }
            catch (Exception e)
            {
                var exceptionMsg = "An internal error occurred.";
                _logService.LogWarning(exceptionMsg);
                return new HttpServiceResponse<T>(default(T), exceptionMsg, HttpStatusCode.InternalServerError, false);
            }
        }

        private Uri CreateBaseUri(string address)
        {
            return new Uri(Uri.UriSchemeHttps + Uri.SchemeDelimiter + address);
        }
    }
}
