using CMS.DataEngine;

namespace SAP
{
    /// <summary>
    /// Class providing <see cref="ChapterInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IChapterInfoProvider))]
    public partial class ChapterInfoProvider : AbstractInfoProvider<ChapterInfo, ChapterInfoProvider>, IChapterInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChapterInfoProvider"/> class.
        /// </summary>
        public ChapterInfoProvider()
            : base(ChapterInfo.TYPEINFO)
        {
        }
    }
}