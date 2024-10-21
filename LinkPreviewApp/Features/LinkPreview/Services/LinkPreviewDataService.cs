using LinkPreviewApp.Common.Http;
using LinkPreviewApp.Models;

namespace LinkPreviewApp.Features.LinkPreview.Services
{
    public class LinkPreviewDataService : ILinkPreviewDataService
    {
        private readonly IHttpService _httpService;

        public LinkPreviewDataService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<LinkPreviewDataModel> FetchLinkPreviewData(string link)
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", "image_x,icon_type,locale" },
                { "q", Uri.EscapeDataString(link) }
            };
            var result = await _httpService.GetAsync<LinkPreviewDataModel>(GlobalConfiguration.LINKPREVIEW_BASE, GlobalConfiguration.LINKPREVIEW_API_KEY_HEADER, link, parameters);
            return result.Content != null ? result.Content : new LinkPreviewDataModel() { Error = result.Error };
        }
    }
}
