using Sap.API.EF.EntityFramework.Context;
using SAP.Models.Interfaces;
using SAP.Models.SaP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sap.API.EF.EntityFramework.Implementations
{
    public class PageRepository : IPageRepository
    {
        public PageRepository(InfoPageContext infoPageContext)
        {
            InfoPageContext = infoPageContext;
        }

        public InfoPageContext InfoPageContext { get; }

        public IEnumerable<NavigationItem> GetNavigation()
        {
            var Pages = InfoPageContext.InfoPages.OrderBy(x => x.NodeOrder).ToList();
            return Pages
                .Select(x => new NavigationItem()
                {
                    PageIdentifier = x.NodeGuid,
                    Title = x.InfoPageTitle,
                    URL = null
                });
        }

        public Page GetPage(Guid PageIdentifier)
        {
            return InfoPageContext.InfoPages.Where(x => x.NodeGuid == PageIdentifier)
                .Select(x => new Page()
                {
                    HtmlContent = x.InfoPageContent,
                    Title = x.InfoPageTitle,
                    PageIdentifier = x.NodeGuid
                }).FirstOrDefault();
        }
    }
}
