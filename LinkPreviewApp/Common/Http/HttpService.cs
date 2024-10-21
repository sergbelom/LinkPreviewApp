using System.Net;
using LinkPreviewApp.Resources;
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
                        _logService.LogWarning(AppResources.InfoMessage_RecivedEvaluatedResponse);
                        return new HttpServiceResponse<T>(evaluatedResponse, string.Empty, response.StatusCode, response.IsSuccessStatusCode);
                    }
                    else
                    {
                        _logService.LogWarning(AppResources.WarningMessage_UnableRecognizeResponseFromService);
                        return new HttpServiceResponse<T>(default(T), AppResources.WarningMessage_UnableRecognizeResponseFromService, HttpStatusCode.InternalServerError, false);
                    }
                }
                else
                {
                    _logService.LogWarning(AppResources.ErrorMessage_NoResponseFromService);
                    return new HttpServiceResponse<T>(default(T), AppResources.ErrorMessage_NoResponseFromService, HttpStatusCode.InternalServerError, false);
                }
            }
            catch (Exception e)
            {
                _logService.LogWarning(AppResources.ErrorMessage_AnInternalErrorOccurred);
                return new HttpServiceResponse<T>(default(T), AppResources.ErrorMessage_AnInternalErrorOccurred, HttpStatusCode.InternalServerError, false);
            }
        }

        private Uri CreateBaseUri(string address)
        {
            return new Uri(Uri.UriSchemeHttps + Uri.SchemeDelimiter + address);
        }
    }
}
