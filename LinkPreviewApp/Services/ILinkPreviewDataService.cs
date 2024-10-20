using LinkPreviewApp.Models;

namespace LinkPreviewApp.Services;

public interface ILinkPreviewDataService
{
    Task<LinkPreviewDataModel> FetchLinkPreviewData(string link);
}
