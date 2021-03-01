using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using SAP;

[assembly: RegisterObjectType(typeof(EpisodeInfo), EpisodeInfo.OBJECT_TYPE)]

namespace SAP
{
    /// <summary>
    /// Data container class for <see cref="EpisodeInfo"/>.
    /// </summary>
    [Serializable]
    public partial class EpisodeInfo : AbstractInfo<EpisodeInfo, IEpisodeInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "sap.episode";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(EpisodeInfoProvider), OBJECT_TYPE, "SAP.Episode", "EpisodeID", "EpisodeLastModified", "EpisodeGuid", null, "EpisodeTitle", null, null, null, null)
        {
            ModuleName = "SAP",
            TouchCacheDependencies = true,
            DependsOn = new List<ObjectDependency>()
            {
                new ObjectDependency("EpisodeChapterID", "sap.chapter", ObjectDependencyEnum.Required),
            },
        };


        /// <summary>
        /// Episode ID.
        /// </summary>
        [DatabaseField]
        public virtual int EpisodeID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("EpisodeID"), 0);
            }
            set
            {
                SetValue("EpisodeID", value);
            }
        }


        /// <summary>
        /// What episode number is this.
        /// </summary>
        [DatabaseField]
        public virtual int EpisodeNumber
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("EpisodeNumber"), 0);
            }
            set
            {
                SetValue("EpisodeNumber", value);
            }
        }


        /// <summary>
        /// There is one section of comics where there are 10 episode 425s, this is to handle that..
        /// </summary>
        [DatabaseField]
        public virtual int EpisodeSubNumber
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("EpisodeSubNumber"), 0);
            }
            set
            {
                SetValue("EpisodeSubNumber", value, 0);
            }
        }


        /// <summary>
        /// Episode title.
        /// </summary>
        [DatabaseField]
        public virtual string EpisodeTitle
        {
            get
            {
                return ValidationHelper.GetString(GetValue("EpisodeTitle"), String.Empty);
            }
            set
            {
                SetValue("EpisodeTitle", value);
            }
        }


        /// <summary>
        /// Episode file url.
        /// </summary>
        [DatabaseField]
        public virtual string EpisodeFileUrl
        {
            get
            {
                return ValidationHelper.GetString(GetValue("EpisodeFileUrl"), String.Empty);
            }
            set
            {
                SetValue("EpisodeFileUrl", value);
            }
        }


        /// <summary>
        /// Episode chapter ID.
        /// </summary>
        [DatabaseField]
        public virtual int EpisodeChapterID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("EpisodeChapterID"), 0);
            }
            set
            {
                SetValue("EpisodeChapterID", value);
            }
        }


        /// <summary>
        /// Episode commentary.
        /// </summary>
        [DatabaseField]
        public virtual string EpisodeCommentary
        {
            get
            {
                return ValidationHelper.GetString(GetValue("EpisodeCommentary"), String.Empty);
            }
            set
            {
                SetValue("EpisodeCommentary", value, String.Empty);
            }
        }


        /// <summary>
        /// Episode date.
        /// </summary>
        [DatabaseField]
        public virtual DateTime EpisodeDate
        {
            get
            {
                return ValidationHelper.GetDate(GetValue("EpisodeDate"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("EpisodeDate", value);
            }
        }


        /// <summary>
        /// If the episode is a animation.
        /// </summary>
        [DatabaseField]
        public virtual bool EpisodeIsAnimation
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("EpisodeIsAnimation"), false);
            }
            set
            {
                SetValue("EpisodeIsAnimation", value);
            }
        }


        /// <summary>
        /// Episode guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid EpisodeGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("EpisodeGuid"), Guid.Empty);
            }
            set
            {
                SetValue("EpisodeGuid", value);
            }
        }


        /// <summary>
        /// Episode last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime EpisodeLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("EpisodeLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("EpisodeLastModified", value);
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
        protected EpisodeInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="EpisodeInfo"/> class.
        /// </summary>
        public EpisodeInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="EpisodeInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public EpisodeInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}