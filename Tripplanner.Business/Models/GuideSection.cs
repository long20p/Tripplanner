using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public class GuideSection
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public int Level { get; set; }
        public string Title { get; set; }
        public string PageId { get; set; }
        public string HtmlContent { get; set; }
    }
}
