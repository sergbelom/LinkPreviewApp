using LinkPreviewApp.Common.Http;
using LinkPreviewApp.Features.LinkPreview.Services;
using LinkPreviewApp.Features.LinkPreview.ViewModels;
using LinkPreviewApp.Services;
using Microsoft.Extensions.Logging;

namespace LinkPreviewApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<IHttpClientService, HttpClientService>();
            mauiAppBuilder.Services.AddTransient<IHttpService, HttpService>();
            mauiAppBuilder.Services.AddTransient<ILinkPreviewDataService, LinkPreviewDataService>();
            mauiAppBuilder.Services.AddTransient<INavigationService, NavigationService>();
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainPageViewModel>();
            mauiAppBuilder.Services.AddSingleton<LinkPreviewViewModel>();            
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainPage>();
            return mauiAppBuilder;
        }
    }
}
