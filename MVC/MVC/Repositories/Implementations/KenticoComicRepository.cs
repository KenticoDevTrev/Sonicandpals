using CMS.CustomTables;
using CMS.CustomTables.Types.Sap;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Sap;
using CMS.Membership;
using Generic.Controllers;
using Sap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sonicandpals.Repositories.Implementations
{
    public class KenticoComicRepository : IComicRepository
    {

        public Comic GetTodaysComic()
        {

            
            var EpisodeItem = new DocumentQuery<Episodes_PageType>()
                .OrderBy("EpisodeNumber desc")
                .FirstOrDefault();



            return new Comic()
            {
                Title = EpisodeItem.Title,
                Commentary = EpisodeItem.Commentary,
                Date = EpisodeItem.Date,
                Chapter = EpisodeItem.Chapter,
                EpisodeNumber = EpisodeItem.EpisodeNumber,
                ImageUrl = EpisodeItem.ImageUrl
            };
        }
    }
}