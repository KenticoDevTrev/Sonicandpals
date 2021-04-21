using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sap.API.EF.EntityFramework.Models
{
    [Table("SAP_EpisodeRating")]
    public class EpisodeRatingEf
    {
        [Key]
        public int EpisodeRatingID { get; set; }
        public int EpisodeRatingEpisodeID { get; set; }
        [ForeignKey(nameof(EpisodeRatingEpisodeID))]
        public EpisodeEf Episode { get; set; }
        public int EpisodeRatingValue { get; set; }
        public string EpisodeRatingIP { get; set; }
        public DateTime EpisodeRatingLastModified { get; set; } = DateTime.Now;
    }
}
