using Microsoft.EntityFrameworkCore;
using Sap.API.EF.EntityFramework.Context;
using SAP.Models.Interfaces;
using SAP.Models.SaP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sap.API.EF.EntityFramework.Implementations
{
    public class ChapterRepository : IChapterRepository
    {
        public ChapterRepository(ChapterContext chapterContext)
        {
            ChapterContext = chapterContext;
        }

        public ChapterContext ChapterContext { get; }

        public IEnumerable<Chapter> GetChapters()
        {
            var Chapters = ChapterContext
                .Chapters
                .OrderBy(x => x.ChapterStartEpisodeNumber)
                .ToListAsync()
                .Result;
            return Chapters.Select(x => new Chapter()
            {
                Title = x.ChapterTitle,
                StartDate = x.ChapterStartDate,
                StartEpisodeNumber = x.ChapterStartEpisodeNumber,
                Code = x.ChapterCodeName
            });
        }

        public string GetChapterTitle(int ChapterID)
        {
            var ChapterItem = ChapterContext.Chapters
                .Where(x => x.ChapterID == ChapterID)
                .FirstOrDefaultAsync()
                .Result;
            return ChapterItem?.ChapterTitle;
        }

        public string GetChapterTitle(string ChapterCode)
        {
            var ChapterItem = ChapterContext.Chapters
                .Where(x => x.ChapterCodeName.Equals(ChapterCode, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefaultAsync()
                .Result;
            return ChapterItem?.ChapterTitle;
        }
    }
}
