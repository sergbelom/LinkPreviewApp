namespace LinkPreviewApp.Services
{
    /// <summary>
    /// Service for navigating through pages, popups, opening defult browser
    /// </summary>
    public class NavigationService : INavigationService
    {
        //possible improvement: using INavigation features for open pages, popups etc.

        private readonly AppLogService _logService;

        public NavigationService(AppLogService logService)
        {
            _logService = logService;
        }


        /// <summary>
        /// Open Default Browser
        /// </summary>
        /// <param name="_linkPreviewUrl"></param>
        /// <returns></returns>
        public async Task OpenBrowserAsync(string _linkPreviewUrl)
        {
            try
            {
                var uri = new Uri(_linkPreviewUrl);
                var options = new BrowserLaunchOptions()
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Colors.Violet,
                    PreferredControlColor = Colors.SandyBrown
                };

                await Browser.Default.OpenAsync(uri, options);
            }
            catch (Exception ex)
            {
                _logService.LogError($"An unexpected error occurred:{ex.Message}");
            }
        }
    }
}
