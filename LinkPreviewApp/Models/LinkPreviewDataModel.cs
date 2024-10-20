using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkPreviewApp.Models
{
    public class LinkPreviewDataModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public int ImageX { get; set; }
        public string IconType { get; set; }
        public string Locale { get; set; }
    }
}
