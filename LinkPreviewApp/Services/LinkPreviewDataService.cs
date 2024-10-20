using LinkPreviewApp.Common.Http;
using LinkPreviewApp.Models;

namespace LinkPreviewApp.Services
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
            string baseUrl = "https://api.linkpreview.net/";
            var result = await _httpService.SendAsync<LinkPreviewDataModel>(baseUrl, link);
            return result.Content;        
        }
    }
}
