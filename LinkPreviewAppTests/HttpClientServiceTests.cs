using LinkPreviewApp.Common.Http;

namespace LinkPreviewAppTests
{
    [TestFixture]
    public class HttpClientServiceTests
    {
        private HttpClientService _httpClientService;

        [SetUp]
        public void SetUp()
        {
            _httpClientService = new HttpClientService();
        }

        [Test]
        public void CreateHttpClient_ShouldSetBaseAddressAndTimeout()
        {
            // Arrange
            var baseAddress = new Uri("https://test.test");
            var timeOut = 60;

            // Act
            var httpClient = _httpClientService.CreateHttpClient(baseAddress, timeOut);

            // Assert
            Assert.AreEqual(baseAddress, httpClient.BaseAddress, "BaseAddress should be correctly assigned.");
            Assert.AreEqual(TimeSpan.FromSeconds(timeOut), httpClient.Timeout, "Timeout should be correctly assigned.");
        }

        [Test]
        public void CreateHttpClient_ShouldSetDefaultTimeout_WhenNotSpecified()
        {
            // Arrange
            var baseAddress = new Uri("https://test.test");

            // Act
            var httpClient = _httpClientService.CreateHttpClient(baseAddress);

            // Assert
            Assert.AreEqual(TimeSpan.FromSeconds(120), httpClient.Timeout, "Default timeout should be 120 seconds.");
        }
    }
}
