namespace SAP.Models.Interfaces
{
    public interface IChapterRepository
    {
        string GetChapterTitle(int ChapterID);

        string GetChapterTitle(string ChapterCode);
    }
}