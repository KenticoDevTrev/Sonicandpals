﻿//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at https://docs.xperience.io/.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CMS;
using CMS.Base;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.SaP;

[assembly: RegisterDocumentType(InfoPage.CLASS_NAME, typeof(InfoPage))]

namespace CMS.DocumentEngine.Types.SaP
{
	/// <summary>
	/// Represents a content item of type InfoPage.
	/// </summary>
	public partial class InfoPage : TreeNode
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "SaP.InfoPage";


		/// <summary>
		/// The instance of the class that provides extended API for working with InfoPage fields.
		/// </summary>
		private readonly InfoPageFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// InfoPageID.
		/// </summary>
		[DatabaseIDField]
		public int InfoPageID
		{
			get
			{
				return ValidationHelper.GetInteger(GetValue("InfoPageID"), 0);
			}
			set
			{
				SetValue("InfoPageID", value);
			}
		}


		/// <summary>
		/// Title.
		/// </summary>
		[DatabaseField]
		public string InfoPageTitle
		{
			get
			{
				return ValidationHelper.GetString(GetValue("InfoPageTitle"), @"");
			}
			set
			{
				SetValue("InfoPageTitle", value);
			}
		}


		/// <summary>
		/// Content.
		/// </summary>
		[DatabaseField]
		public string InfoPageContent
		{
			get
			{
				return ValidationHelper.GetString(GetValue("InfoPageContent"), @"");
			}
			set
			{
				SetValue("InfoPageContent", value);
			}
		}


		/// <summary>
		/// Gets an object that provides extended API for working with InfoPage fields.
		/// </summary>
		[RegisterProperty]
		public InfoPageFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with InfoPage fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class InfoPageFields : AbstractHierarchicalObject<InfoPageFields>
		{
			/// <summary>
			/// The content item of type InfoPage that is a target of the extended API.
			/// </summary>
			private readonly InfoPage mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="InfoPageFields" /> class with the specified content item of type InfoPage.
			/// </summary>
			/// <param name="instance">The content item of type InfoPage that is a target of the extended API.</param>
			public InfoPageFields(InfoPage instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// InfoPageID.
			/// </summary>
			public int ID
			{
				get
				{
					return mInstance.InfoPageID;
				}
				set
				{
					mInstance.InfoPageID = value;
				}
			}


			/// <summary>
			/// Title.
			/// </summary>
			public string Title
			{
				get
				{
					return mInstance.InfoPageTitle;
				}
				set
				{
					mInstance.InfoPageTitle = value;
				}
			}


			/// <summary>
			/// Content.
			/// </summary>
			public string Content
			{
				get
				{
					return mInstance.InfoPageContent;
				}
				set
				{
					mInstance.InfoPageContent = value;
				}
			}
		}

		#endregion


		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="InfoPage" /> class.
		/// </summary>
		public InfoPage() : base(CLASS_NAME)
		{
			mFields = new InfoPageFields(this);
		}

		#endregion
	}
}