using CMS.DataEngine;

namespace SAP
{
    /// <summary>
    /// Class providing <see cref="EpisodeInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IEpisodeInfoProvider))]
    public partial class EpisodeInfoProvider : AbstractInfoProvider<EpisodeInfo, EpisodeInfoProvider>, IEpisodeInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeInfoProvider"/> class.
        /// </summary>
        public EpisodeInfoProvider()
            : base(EpisodeInfo.TYPEINFO)
        {
        }
    }
}