using NSubstitute;
using LinkPreviewApp.Services;
using LinkPreviewApp.Features.LinkPreview.Services;
using LinkPreviewApp.Features.LinkPreview.ViewModels;
using LinkPreviewApp.Features.LinkPreview.Models;

namespace LinkPreviewAppTests
{
    [TestFixture]
    public class LinkPreviewViewModelTests
    {
        private LinkPreviewViewModel _viewModel;
        private ILinkPreviewDataService _linkPreviewDataService;
        private INavigationService _navigationService;

        [SetUp]
        public void SetUp()
        {
            _linkPreviewDataService = Substitute.For<ILinkPreviewDataService>();
            _navigationService = Substitute.For<INavigationService>();
            _viewModel = new LinkPreviewViewModel(_linkPreviewDataService, _navigationService);
        }

        [Test]
        public async Task CreatePreviewAsync_WhenServiceReturnsValidData_ShouldUpdateViewModelProperties()
        {
            // arrange
            _viewModel.RawLinkText = "https://test.test";
            var validPreviewData = new LinkPreviewDataModel
            {
                Image = "https://test.test/test.logo",
                Title = "Example Title",
                Description = "Example Description",
                Url = "https://test.test"
            };

            _linkPreviewDataService.FetchLinkPreviewData(Arg.Any<string>())
                .Returns(Task.FromResult(validPreviewData));

            // act
            _viewModel.CreatePreviewCommand.Execute(null);

            // assert
            Assert.IsFalse(_viewModel.IsLoading);
            Assert.IsTrue(_viewModel.LinkPreviewIsVisible);
            Assert.IsFalse(_viewModel.ErrorIsVisible);
            Assert.AreEqual(_viewModel.ImageUri, validPreviewData.Image);
            Assert.AreEqual(_viewModel.LinkTitle, validPreviewData.Title);
            Assert.AreEqual(_viewModel.LinkDescripton, validPreviewData.Description);
        }

        [Test]
        public async Task CreatePreviewAsync_WhenServiceReturnsError_ShouldShowErrorMessage()
        {
            // arrange
            _viewModel.RawLinkText = "https://test.test";
            var errorData = new LinkPreviewDataModel
            {
                Error = "Error fetching link preview"
            };

            _linkPreviewDataService.FetchLinkPreviewData(Arg.Any<string>())
                .Returns(Task.FromResult(errorData));

            // act
            _viewModel.CreatePreviewCommand.Execute(null);

            // assert
            Assert.IsFalse(_viewModel.IsLoading);
            Assert.IsFalse(_viewModel.LinkPreviewIsVisible);
            Assert.IsTrue(_viewModel.ErrorIsVisible);
            Assert.AreEqual(_viewModel.ErrorMessage, "Error fetching link preview");
        }

        [Test]
        public async Task CreatePreviewAsync_WhenServiceReturnsNull_ShouldShowNoResponseError()
        {
            // arrange
            _viewModel.RawLinkText = "https://test.test";

            _linkPreviewDataService.FetchLinkPreviewData(Arg.Any<string>())
                .Returns(Task.FromResult<LinkPreviewDataModel>(null));

            // act
            _viewModel.CreatePreviewCommand.Execute(null);

            // assert
            Assert.IsFalse(_viewModel.IsLoading);
            Assert.IsFalse(_viewModel.LinkPreviewIsVisible);
            Assert.IsTrue(_viewModel.ErrorIsVisible);
            Assert.AreEqual(_viewModel.ErrorMessage, "No response from service.");
        }

        [Test]
        public async Task LinkPreviewTappedAsync_WhenUrlIsAvailable_ShouldNavigateToBrowser()
        {
            // arrange
            _viewModel.RawLinkText = "https://test.test";
            var validPreviewData = new LinkPreviewDataModel
            {
                Url = "https://test.test",
                Image = "https://test.test/test.logo"
            };

            _linkPreviewDataService.FetchLinkPreviewData(Arg.Any<string>())
                .Returns(Task.FromResult(validPreviewData));

            _viewModel.CreatePreviewCommand.Execute(null);

            // act
            _viewModel.LinkPreviewTappedCommand.Execute("https://test.test");

            // assert
            await _navigationService.Received(1).OpenBrowserAsync("https://test.test");
        }

        [Test]
        public async Task LinkPreviewTappedAsync_WhenUrlIsNull_ShouldNotNavigate()
        {
            // arrange
            _viewModel.RawLinkText = "https://test.test";
            var previewDataWithNoUrl = new LinkPreviewDataModel
            {
                Url = string.Empty,
                Image = string.Empty                
            };

            _linkPreviewDataService.FetchLinkPreviewData(Arg.Any<string>())
                .Returns(Task.FromResult(previewDataWithNoUrl));

            _viewModel.CreatePreviewCommand.Execute(null);

            // act
            _viewModel.LinkPreviewTappedCommand.Execute(null);

            // assert
            await _navigationService.DidNotReceive().OpenBrowserAsync(Arg.Any<string>());
        }
    }
}
