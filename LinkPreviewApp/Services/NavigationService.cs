using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkPreviewApp.Services
{
    internal class NavigationService : INavigationService
    {
        private INavigation _navigation;

        public async Task OpenBrowserAsync(string _linkPreviewUrl)
        {
            try
            {
                Uri uri = new Uri(_linkPreviewUrl);
                BrowserLaunchOptions options = new BrowserLaunchOptions()
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
                // An unexpected error occurred. No browser may be installed on the device.
            }
        }
    }
}
