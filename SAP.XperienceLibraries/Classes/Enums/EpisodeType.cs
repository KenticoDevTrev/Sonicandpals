using CMS;
using SAP;

[assembly: RegisterCustomClass("EpisodeType", typeof(EpisodeType))]

namespace SAP
{
    public enum EpisodeType
    {
        Image, Video
    }
}
