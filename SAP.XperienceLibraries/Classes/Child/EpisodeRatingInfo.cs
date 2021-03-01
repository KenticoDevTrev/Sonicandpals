using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using SAP;

[assembly: RegisterObjectType(typeof(EpisodeRatingInfo), EpisodeRatingInfo.OBJECT_TYPE)]

namespace SAP
{
    /// <summary>
    /// Data container class for <see cref="EpisodeRatingInfo"/>.
    /// </summary>
    [Serializable]
    public partial class EpisodeRatingInfo : AbstractInfo<EpisodeRatingInfo, IEpisodeRatingInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "sap.episoderating";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(EpisodeRatingInfoProvider), OBJECT_TYPE, "SAP.EpisodeRating", "EpisodeRatingID", "EpisodeRatingLastModified", null, null, null, null, null, null, null)
        {
            ModuleName = "SAP",
            TouchCacheDependencies = true,
            DependsOn = new List<ObjectDependency>()
            {
                new ObjectDependency("EpisodeRatingEpisodeID", "sap.episode", ObjectDependencyEnum.Required),
            },
        };


        /// <summary>
        /// Episode rating ID.
        /// </summary>
        [DatabaseField]
        public virtual int EpisodeRatingID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("EpisodeRatingID"), 0);
            }
            set
            {
                SetValue("EpisodeRatingID", value);
            }
        }


        /// <summary>
        /// Episode rating episode ID.
        /// </summary>
        [DatabaseField]
        public virtual int EpisodeRatingEpisodeID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("EpisodeRatingEpisodeID"), 0);
            }
            set
            {
                SetValue("EpisodeRatingEpisodeID", value);
            }
        }


        /// <summary>
        /// 1-5.
        /// </summary>
        [DatabaseField]
        public virtual int EpisodeRatingValue
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("EpisodeRatingValue"), 0);
            }
            set
            {
                SetValue("EpisodeRatingValue", value);
            }
        }


        /// <summary>
        /// Episode rating IP.
        /// </summary>
        [DatabaseField]
        public virtual string EpisodeRatingIP
        {
            get
            {
                return ValidationHelper.GetString(GetValue("EpisodeRatingIP"), String.Empty);
            }
            set
            {
                SetValue("EpisodeRatingIP", value);
            }
        }


        /// <summary>
        /// Episode rating last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime EpisodeRatingLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("EpisodeRatingLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("EpisodeRatingLastModified", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            Provider.Delete(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            Provider.Set(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected EpisodeRatingInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="EpisodeRatingInfo"/> class.
        /// </summary>
        public EpisodeRatingInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="EpisodeRatingInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public EpisodeRatingInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}