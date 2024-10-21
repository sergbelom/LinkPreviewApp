using Newtonsoft.Json;

namespace LinkPreviewApp.Common.Http
{
    /// <summary>
    /// Mock Http Service
    /// Motivation: use mock response for automation.
    /// </summary>
    public class MockHttpService : IHttpService
    {
        /// <summary>
        /// Get mock response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseAddress"></param>
        /// <param name="header"></param>
        /// <param name="apiKey"></param>
        /// <param name="link"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public Task<HttpServiceResponse<T>> GetAsync<T>(string baseAddress, string header, string apiKey, string link, Dictionary<string, string> queryParams) where T : class
        {
            Task.Delay(1000); // emulate timeout
            var dummyResponse = "{\"title\":\"Google\",\"description\":\"Common Info \",\"image\":\"http://www.google.com/images/branding/googlelogo/1x/googlelogo_white_background_color_272x92dp.png\",\"url\":\"http://www.google.com/\"}\n";
            var evaluatedResponse = JsonConvert.DeserializeObject<T>(dummyResponse);
            return Task.FromResult(new HttpServiceResponse<T>(evaluatedResponse, string.Empty, System.Net.HttpStatusCode.OK, true));
        }
    }
}
