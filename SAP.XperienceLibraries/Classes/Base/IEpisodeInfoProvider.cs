using CMS.DataEngine;

namespace SAP
{
    /// <summary>
    /// Declares members for <see cref="EpisodeInfo"/> management.
    /// </summary>
    public partial interface IEpisodeInfoProvider : IInfoProvider<EpisodeInfo>, IInfoByIdProvider<EpisodeInfo>, IInfoByGuidProvider<EpisodeInfo>
    {
    }
}