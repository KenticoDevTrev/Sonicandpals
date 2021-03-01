using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using SAP;

[assembly: RegisterObjectType(typeof(ChapterInfo), ChapterInfo.OBJECT_TYPE)]

namespace SAP
{
    /// <summary>
    /// Data container class for <see cref="ChapterInfo"/>.
    /// </summary>
    [Serializable]
    public partial class ChapterInfo : AbstractInfo<ChapterInfo, IChapterInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "sap.chapter";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(ChapterInfoProvider), OBJECT_TYPE, "SAP.Chapter", "ChapterID", "ChapterLastModified", "ChapterGuid", "ChapterCodeName", "ChapterTitle", null, null, null, null)
        {
            ModuleName = "SAP",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Chapter ID.
        /// </summary>
        [DatabaseField]
        public virtual int ChapterID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("ChapterID"), 0);
            }
            set
            {
                SetValue("ChapterID", value);
            }
        }


        /// <summary>
        /// Chapter title.
        /// </summary>
        [DatabaseField]
        public virtual string ChapterTitle
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ChapterTitle"), String.Empty);
            }
            set
            {
                SetValue("ChapterTitle", value);
            }
        }


        /// <summary>
        /// Chapter code name.
        /// </summary>
        [DatabaseField]
        public virtual string ChapterCodeName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ChapterCodeName"), String.Empty);
            }
            set
            {
                SetValue("ChapterCodeName", value);
            }
        }


        /// <summary>
        /// Chapter start date.
        /// </summary>
        [DatabaseField]
        public virtual DateTime ChapterStartDate
        {
            get
            {
                return ValidationHelper.GetDate(GetValue("ChapterStartDate"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("ChapterStartDate", value);
            }
        }


        /// <summary>
        /// Chapter start episode number.
        /// </summary>
        [DatabaseField]
        public virtual int ChapterStartEpisodeNumber
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("ChapterStartEpisodeNumber"), 0);
            }
            set
            {
                SetValue("ChapterStartEpisodeNumber", value);
            }
        }


        /// <summary>
        /// Chapter guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid ChapterGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("ChapterGuid"), Guid.Empty);
            }
            set
            {
                SetValue("ChapterGuid", value);
            }
        }


        /// <summary>
        /// Chapter last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime ChapterLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("ChapterLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("ChapterLastModified", value);
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
        protected ChapterInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="ChapterInfo"/> class.
        /// </summary>
        public ChapterInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="ChapterInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public ChapterInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}