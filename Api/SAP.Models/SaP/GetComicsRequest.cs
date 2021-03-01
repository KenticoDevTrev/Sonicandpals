using System;

namespace SAP.Models
{
    public class GetComicsRequest
    {
        public int? EpisodeNumber { get; set; }
        public int? EpisodeSubNumber { get; set; }
        public DateTime? Date { get; set; }
    }
}