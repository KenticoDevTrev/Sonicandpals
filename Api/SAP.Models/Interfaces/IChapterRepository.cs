using SAP.Models.SaP;
using System.Collections.Generic;

namespace SAP.Models.Interfaces
{
    public interface IChapterRepository
    {
        string GetChapterTitle(int ChapterID);

        string GetChapterTitle(string ChapterCode);

        IEnumerable<Chapter> GetChapters();
    }
}