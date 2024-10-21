namespace LinkPreviewApp.Services
{
    public interface INavigationService
    {
        Task OpenBrowserAsync(string url);
    }
}
