using CMS.DataEngine;

namespace SAP
{
    /// <summary>
    /// Declares members for <see cref="ChapterInfo"/> management.
    /// </summary>
    public partial interface IChapterInfoProvider : IInfoProvider<ChapterInfo>, IInfoByIdProvider<ChapterInfo>, IInfoByNameProvider<ChapterInfo>, IInfoByGuidProvider<ChapterInfo>
    {
    }
}