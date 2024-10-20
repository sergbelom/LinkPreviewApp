using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkPreviewApp.Common.Http;

public interface IHttpClientService
{
    HttpClient CreateHttpClient(int timeOut = 120);
}
