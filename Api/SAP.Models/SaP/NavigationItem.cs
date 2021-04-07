using System;

namespace SAP.Models.SaP
{
    public class NavigationItem
    {
        public string Title { get; set; }
        public Guid PageIdentifier { get; set; }
        public string URL { get; set; }
    }
}
