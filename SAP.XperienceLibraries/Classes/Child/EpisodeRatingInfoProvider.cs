using CMS.DataEngine;

namespace SAP
{
    /// <summary>
    /// Class providing <see cref="EpisodeRatingInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IEpisodeRatingInfoProvider))]
    public partial class EpisodeRatingInfoProvider : AbstractInfoProvider<EpisodeRatingInfo, EpisodeRatingInfoProvider>, IEpisodeRatingInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeRatingInfoProvider"/> class.
        /// </summary>
        public EpisodeRatingInfoProvider()
            : base(EpisodeRatingInfo.TYPEINFO)
        {
        }
    }
}