using CMS.Helpers;
using SAP.Models.Interfaces;
using SAP.Models.SaP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SAP.Library.Implementations
{
    public class ChapterRepository : IChapterRepository
    {
        public ChapterRepository(IChapterInfoProvider chapterInfoProvider)
        {
            ChapterInfoProvider = chapterInfoProvider;
        }

        public IChapterInfoProvider ChapterInfoProvider { get; }

        public IEnumerable<Chapter> GetChapters()
        {
            return CacheHelper.Cache(cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = CacheHelper.GetCacheDependency(new string[] { $"{ChapterInfo.OBJECT_TYPE}|all" });
                }
                return ChapterInfoProvider.Get()
                .OrderBy(nameof(ChapterInfo.ChapterStartEpisodeNumber))
                .Select(x => new Chapter()
                {
                    Title = x.ChapterTitle,
                    Code = x.ChapterCodeName,
                    StartDate = x.ChapterStartDate,
                    StartEpisodeNumber = x.ChapterStartEpisodeNumber
                });
            }, new CacheSettings(1440, "GetChapters"));
        }

        public string GetChapterTitle(int ChapterID)
        {
            return ChapterInfoProvider.Get(ChapterID).ChapterTitle;
        }

        public string GetChapterTitle(string ChapterCode)
        {
            return ChapterInfoProvider.Get(ChapterCode).ChapterTitle;
        }
    }
}
