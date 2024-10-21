using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkPreviewApp
{
    //[assembly: InternalsVisibleTo("LinkPreviewAppTests")]
    internal class GlobalConfiguration
    {
        public const string LINKPREVIEW_BASE = "api.linkpreview.net";
        public const string LINKPREVIEW_API_KEY_HEADER = "X-Linkpreview-Api-Key";


    }
}
