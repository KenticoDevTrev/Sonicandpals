using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sap.API.EF.EntityFramework.Models
{
    [Table("SAP_Chapter")]

    public class ChapterEf
    {
        [Key]
        public int ChapterID { get; set; }
        public string ChapterTitle { get; set; }

        public string ChapterCodeName { get; set; }

        public DateTime ChapterStartDate { get; set; }

        public int ChapterStartEpisodeNumber { get; set; }
        public Guid ChapterGuid { get; set; }
        public DateTime ChapterLastModified { get; set; }
    }


}
