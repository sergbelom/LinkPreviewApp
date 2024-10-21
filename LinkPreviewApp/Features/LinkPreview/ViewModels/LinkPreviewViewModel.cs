using LinkPreviewApp.Common;
using LinkPreviewApp.Features.LinkPreview.Services;
using LinkPreviewApp.Services;
using System.Windows.Input;

namespace LinkPreviewApp.Features.LinkPreview.ViewModels
{
    public class LinkPreviewViewModel : BaseViewModel
    {
        private string _linkPreviewUrl;

        private readonly ILinkPreviewDataService _linkPreviewDataService;
        private readonly INavigationService _navigationService;

        private string _rawLinkText;
        public string RawLinkText
        {
            get => _rawLinkText;
            set => SetValue(ref _rawLinkText, value);
        }

        private bool _linkPreviewIsVisible;
        public bool LinkPreviewIsVisible
        {
            get => _linkPreviewIsVisible;
            set => SetValue(ref _linkPreviewIsVisible, value);
        }

        private string _imageUri;
        public string ImageUri
        {
            get => _imageUri;
            set => SetValue(ref _imageUri, value);
        }

        private string _linkTitle;
        public string LinkTitle
        {
            get => _linkTitle;
            set => SetValue(ref _linkTitle, value);
        }

        private string _linkDescripton;
        public string LinkDescripton
        {
            get => _linkDescripton;
            set => SetValue(ref _linkDescripton, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetValue(ref _isLoading, value);
        }

        private bool _errorIsVisible;
        public bool ErrorIsVisible
        {
            get => _errorIsVisible;
            set => SetValue(ref _errorIsVisible, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetValue(ref _errorMessage, value);
        }

        private string _loadingMessage;
        public string LoadingMessage
        {
            get => _loadingMessage;
            set => SetValue(ref _loadingMessage, value);
        }

        public ICommand CreatePreviewCommand { private set; get; }

        public ICommand LinkPreviewTappedCommand { private set; get; }       

        public LinkPreviewViewModel(ILinkPreviewDataService linkPreviewDataService, INavigationService navigationService)
        {

            _linkPreviewDataService = linkPreviewDataService;
            _navigationService = navigationService;

            CreatePreviewCommand = new Command(CreatePreviewAsync);
            LinkPreviewTappedCommand = new Command(LinkPreviewTappedAsync);
        }

        private async void CreatePreviewAsync()
        {
            IsLoading = true;
            ResetMessages();
            LoadingMessage = "Sending request, please wait ... ";
            var result = await _linkPreviewDataService.FetchLinkPreviewData(_rawLinkText);

            if (null == result) {
                IsLoading = false;
                LinkPreviewIsVisible = false;
                ErrorIsVisible = true;
                ErrorMessage = "No response from service.";
            }
            else
            {
                if (!string.IsNullOrEmpty(result.Error))
                {
                    IsLoading = false;
                    LinkPreviewIsVisible = false;
                    ErrorIsVisible = true;
                    ErrorMessage = string.IsNullOrEmpty(result.Description) ? result.Error : result.Description;
                }
                else if (!string.IsNullOrEmpty(result.Image))
                {
                    IsLoading = false;
                    LinkPreviewIsVisible = true;
                    ErrorIsVisible = false;
                    ImageUri = result.Image;
                    LinkTitle = result.Title;
                    LinkDescripton = result.Description;
                    _linkPreviewUrl = result.Url;
                }
            }
        }

        private void ResetMessages()
        {
            LinkPreviewIsVisible = false;
            ErrorIsVisible = false;
            ErrorMessage = string.Empty;
            LoadingMessage = string.Empty;
            ImageUri = string.Empty;
            LinkTitle = string.Empty;
            LinkDescripton = string.Empty;
            _linkPreviewUrl = string.Empty;
        }

        private async void LinkPreviewTappedAsync()
        {
            if (!string.IsNullOrEmpty(_linkPreviewUrl))
            {
                await _navigationService.OpenBrowserAsync(_linkPreviewUrl);
            }
        }
    }
}
