using LinkPreviewApp.Common;
using LinkPreviewApp.Services;
using System.Windows.Input;

namespace LinkPreviewApp
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ILinkPreviewDataService _linkPreviewDataService;

        private string _link = string.Empty;
        public string Link
        {
            get => _link;
            set => SetValue(ref _link, value);
        }

        private Image _imageSourceProp = null;
        public Image ImageSourceProp
        {
            get => _imageSourceProp;
            set => SetValue(ref _imageSourceProp, value);
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

        public ICommand GoToLinkCommand { private set; get; }

        public MainPageViewModel(ILinkPreviewDataService linkPreviewDataService) {

            _linkPreviewDataService = linkPreviewDataService;

            GoToLinkCommand = new Command(GoToLink);
        }

        private async void GoToLink()
        {
            var res = await _linkPreviewDataService.FetchLinkPreviewData(_link);

            if (res != null && !string.IsNullOrEmpty(res.Image)) {
                ImageUri = res.Image;
                LinkTitle = res.Title;
                LinkDescripton = res.Description;
            }
        }
    }
}
