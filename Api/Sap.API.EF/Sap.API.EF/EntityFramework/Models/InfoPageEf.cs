using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sap.API.EF.EntityFramework.Models
{
	[Table("View_Sap_InfoPage")]
    public class InfoPageEf
    {
		[Key]
		public int DocumentID { get; set; }
		public Guid NodeGuid { get; set; }
		public Guid DocumentGuid { get; set; }
		public int NodeOrder { get; set; }
		public int InfoPageID { get; set; }
		public string InfoPageTitle { get; set; }
		public string InfoPageContent { get; set; }
	}
}
