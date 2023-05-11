using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sap.API.EF.EntityFramework.Models
{
    [Table("Sap_Episode")]
    public class EpisodeEf
    {
        [Key]
        public int EpisodeID { get; set; }
        public int EpisodeNumber { get; set; }
        public int? EpisodeSubNumber { get; set; }
        public string EpisodeTitle { get; set; }
        public string EpisodeFileUrl { get; set; }
        public int EpisodeChapterID { get; set; }
        [ForeignKey(nameof(EpisodeChapterID))]
        public ChapterEf Chapter { get; set; }
        public string EpisodeCommentary { get; set; }
        public DateTime EpisodeDate { get; set; }
        public bool EpisodeIsAnimation { get; set; }
        public Guid EpisodeGuid { get; set; }
        public DateTime EpisodeLastModified { get; set; }
        public List<EpisodeRatingEf> Ratings { get; set; }
    }
}
