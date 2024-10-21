using LinkPreviewApp;
using LinkPreviewApp.Common.Http;
using LinkPreviewApp.Features.LinkPreview.Models;
using LinkPreviewApp.Features.LinkPreview.Services;
using NSubstitute;

namespace LinkPreviewAppTests
{
    [TestFixture]
    public class LinkPreviewDataServiceTests
    {
        private LinkPreviewDataService _linkPreviewDataService;
        private IHttpService _httpService;

        [SetUp]
        public void SetUp()
        {
            _httpService = Substitute.For<IHttpService>();
            _linkPreviewDataService = new LinkPreviewDataService(_httpService);
        }

        [Test]
        public async Task FetchLinkPreviewData_WhenApiReturnsValidContent_ShouldReturnContent()
        {
            // arrange
            var link = "https://test.test";
            var expectedData = new LinkPreviewDataModel { Image = "https://test.test/image.jpg" };
            var apiResponse = new HttpServiceResponse<LinkPreviewDataModel>(expectedData, string.Empty, System.Net.HttpStatusCode.OK, true);

            _httpService.GetAsync<LinkPreviewDataModel>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult(apiResponse));

            // act
            var result = await _linkPreviewDataService.FetchLinkPreviewData(link);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedData.Image, result.Image);
        }

        [Test]
        public async Task FetchLinkPreviewData_WhenApiReturnsError_ShouldReturnLinkPreviewDataModelWithError()
        {
            // arrange
            var link = "https://test.test";
            var expectedData = new LinkPreviewDataModel { Error = "404", Description = "Not Found" };
            var apiErrorResponse = new HttpServiceResponse<LinkPreviewDataModel>(expectedData, string.Empty, System.Net.HttpStatusCode.NotFound, false);

            _httpService.GetAsync<LinkPreviewDataModel>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult(apiErrorResponse));

            // act
            var result = await _linkPreviewDataService.FetchLinkPreviewData(link);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Error, "404");
            Assert.AreEqual(result.Description, "Not Found");
        }

        [Test]
        public async Task FetchLinkPreviewData_ShouldCallHttpServiceWithCorrectParameters()
        {
            // arrange
            var link = "https://test.test";
            var expectedData = new LinkPreviewDataModel { Image = "https://test.test/image.jpg" };
            var apiResponse = new HttpServiceResponse<LinkPreviewDataModel>(expectedData, string.Empty, System.Net.HttpStatusCode.OK, true);

            _httpService.GetAsync<LinkPreviewDataModel>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult(apiResponse));

            // act
            await _linkPreviewDataService.FetchLinkPreviewData(link);

            // assert
            await _httpService.Received(1).GetAsync<LinkPreviewDataModel>(
                Arg.Is(GlobalConfiguration.LINKPREVIEW_BASE),
                Arg.Is(GlobalConfiguration.LINKPREVIEW_API_KEY_HEADER),
                Arg.Is(GlobalConfiguration.LINKPREVIEW_API_KEY),
                Arg.Is(link),
                Arg.Is<Dictionary<string, string>>(p =>
                    p["fields"] == "image_x,icon_type,locale" &&
                    p["q"] == Uri.EscapeDataString(link))
            );
        }
    }
}
