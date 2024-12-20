﻿namespace LinkPreviewApp.Common.Http;

public interface IHttpService
{
    Task<HttpServiceResponse<T>> GetAsync<T>(string baseAddress, string header, string apiKey, string link, Dictionary<string, string> queryParams) where T : class;
}
