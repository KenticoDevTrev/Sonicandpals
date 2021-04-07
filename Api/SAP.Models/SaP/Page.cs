using System;

namespace SAP.Models.SaP
{
    public class Page
    {
        public Guid PageIdentifier { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
    }
}
