using LinkPreviewApp.Services;
using Newtonsoft.Json;
using NSubstitute;
using Serilog;
using Serilog.Core;
using System.Net;

namespace LinkPreviewApp.Common.Http.Tests
{
    public class HttpServiceTests
    {
        private IHttpClientService _httpClientService;
        private HttpService _httpService;

        [SetUp]
        public void Setup()
        {
            var mockLogger = new LoggerConfiguration().CreateLogger();
            var appLogService = new AppLogService();
            typeof(AppLogService).GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(appLogService, mockLogger);

            _httpClientService = Substitute.For<IHttpClientService>();
            _httpService = new HttpService(_httpClientService, appLogService);
        }

        [Test]
        public async Task GetAsync_ReturnsSuccessResponse_WhenApiReturnsValidData()
        {
            // arrange
            var baseAddress = "https://test.test";
            var header = "Authorization";
            var link = "check.check";
            var key = "123456";
            var parameters = new Dictionary<string, string>();
            var expectedResponse = new TestModelResponse { Value = "Value", Name = "Test" };
            var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

            var httpClient = new HttpClient(new TestHttpMessageHandler(jsonResponse, HttpStatusCode.OK));
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            _httpClientService.CreateHttpClient(Arg.Any<Uri>()).Returns(httpClient);

            // act
            var response = await _httpService.GetAsync<TestModelResponse>(baseAddress, header, link, key, parameters);

            // assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(expectedResponse.Value, response.Content.Value);
            Assert.AreEqual(expectedResponse.Name, response.Content.Name);
        }

        [Test]
        public async Task GetAsync_ReturnsErrorResponse_WhenExceptionIsThrown()
        {
            // arrange
            var baseAddress = "https://test.test";
            var header = "Authorization";
            var link = "check.check";
            var key = "123456";
            var parameters = new Dictionary<string, string>();

            _httpClientService.CreateHttpClient(Arg.Any<Uri>()).Returns(x => { throw new HttpRequestException("Error"); });

            // Act
            var response = await _httpService.GetAsync<TestModelResponse>(baseAddress, header, key, link, parameters);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsSuccessful);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.IsNotNull(response.Error);
            Assert.AreEqual("An internal error occurred.", response.Error);
        }
    }

    public class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _responseBody;
        private readonly HttpStatusCode _statusCode;

        public TestHttpMessageHandler(string responseBody, HttpStatusCode statusCode)
        {
            _responseBody = responseBody;
            _statusCode = statusCode;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_responseBody)
            };
            return Task.FromResult(response);
        }
    }

    public class TestModelResponse
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}