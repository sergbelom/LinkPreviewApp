using LinkPreviewApp.Services;
using NSubstitute;
using Serilog;
using Serilog.Core;

namespace LinkPreviewAppTests
{
    [TestFixture]
    public class NavigationServiceTests
    {
        private NavigationService _navigationService;

        [SetUp]
        public void SetUp()
        {
            var mockLogger = new LoggerConfiguration().CreateLogger();
            var appLogService = new AppLogService();
            typeof(AppLogService).GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(appLogService, mockLogger);

            _navigationService = new NavigationService(appLogService);
        }

        [Test]
        public async Task OpenBrowserAsync_WhenCalledWithValidUrl_ShouldNotThrowException()
        {
            // arrange
            var validUrl = "https://test.test";

            // act and assert
            Assert.DoesNotThrowAsync(async () => await _navigationService.OpenBrowserAsync(validUrl));
        }

        [Test]
        public async Task OpenBrowserAsync_WhenCalledWithInvalidUrl_ShouldCatchUriFormatException()
        {
            // arrange
            var invalidUrl = "invalid-url";

            // act and assert
            Assert.DoesNotThrowAsync(async () => await _navigationService.OpenBrowserAsync(invalidUrl));
        }
    }
}