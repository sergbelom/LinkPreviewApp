using LinkPreviewApp.Features.LinkPreview.Models;

namespace LinkPreviewApp.Features.LinkPreview.Services;

public interface ILinkPreviewDataService
{
    Task<LinkPreviewDataModel> FetchLinkPreviewData(string link);
}
