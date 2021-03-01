using CMS.DataEngine;

namespace SAP
{
    /// <summary>
    /// Declares members for <see cref="EpisodeRatingInfo"/> management.
    /// </summary>
    public partial interface IEpisodeRatingInfoProvider : IInfoProvider<EpisodeRatingInfo>, IInfoByIdProvider<EpisodeRatingInfo>, IInfoByGuidProvider<EpisodeRatingInfo>
    {
    }
}