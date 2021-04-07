using SAP.Models.SaP;
using System;
using System.Collections.Generic;

namespace SAP.Models.Interfaces
{
    public interface IPageRepository
    {
        IEnumerable<NavigationItem> GetNavigation();

        Page GetPage(Guid PageIdentifier);
    }
}
