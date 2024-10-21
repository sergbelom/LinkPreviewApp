using LinkPreviewApp.Services;

namespace LinkPreviewAppTests
{
    [TestFixture]
    public class NavigationServiceTests
    {
        private NavigationService _navigationService;

        [SetUp]
        public void SetUp()
        {
            _navigationService = new NavigationService();
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