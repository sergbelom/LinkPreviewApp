using LinkPreviewApp.Common.Http;
using LinkPreviewApp.Features.LinkPreview.Models;

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
                { GlobalConfiguration.LINKPREVIEW_FIELDS_QUERY_KEY, GlobalConfiguration.LINKPREVIEW_FIELDS_QUERY_VALUE },
                { GlobalConfiguration.LINKPREVIEW_LINK_QUERY_KEY, Uri.EscapeDataString(link) }
            };
            var result = await _httpService.GetAsync<LinkPreviewDataModel>
                (GlobalConfiguration.LINKPREVIEW_BASE,
                GlobalConfiguration.LINKPREVIEW_API_KEY_HEADER,
                GlobalConfiguration.LINKPREVIEW_API_KEY,
                link,
                parameters);

            return result.Content != null ? result.Content : new LinkPreviewDataModel() { Error = result.Error };
        }
    }
}
