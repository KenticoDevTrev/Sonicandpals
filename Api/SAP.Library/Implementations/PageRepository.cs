using CMS.DocumentEngine.Types.SaP;
using SAP.Models.Interfaces;
using SAP.Models.SaP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CMS.Helpers;

namespace SAP.Library.Implementations
{
    public class PageRepository : IPageRepository
    {
        public PageRepository()
        {

        }
        public IEnumerable<NavigationItem> GetNavigation()
        {
            var PageItems = CacheHelper.Cache(cs =>
            {
                if(cs.Cached)
                {
                    cs.CacheDependency = CacheHelper.GetCacheDependency(new string[] { $"nodes|SAP|{InfoPage.CLASS_NAME}|all" });
                }
                return InfoPageProvider.GetInfoPages()
                .OrderBy(nameof(InfoPage.NodeOrder))
                .Columns(nameof(InfoPage.InfoPageTitle), nameof(InfoPage.NodeGUID))
                .Select(x => new NavigationItem()
                {
                    PageIdentifier = x.NodeGUID,
                    Title = x.InfoPageTitle,
                    URL = null
                });
            }, new CacheSettings(1440, "GetNavigation"));

            return PageItems;
        }

        public Page GetPage(Guid PageIdentifier)
        {
            return CacheHelper.Cache(cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = CacheHelper.GetCacheDependency(new string[] { $"nodes|SAP|{InfoPage.CLASS_NAME}|all" });
                }
                var Page = InfoPageProvider.GetInfoPage(PageIdentifier, "en-US", "SAP")
                .Columns(nameof(InfoPage.NodeGUID), nameof(InfoPage.InfoPageContent), nameof(InfoPage.InfoPageTitle))
                .Select(x => new Page()
                {
                    HtmlContent = x.InfoPageContent,
                    Title = x.InfoPageTitle,
                    PageIdentifier = x.NodeGUID
                }).FirstOrDefault();
                return Page;
            }, new CacheSettings(1440, "GetNavigation"));
        }
    }
}
