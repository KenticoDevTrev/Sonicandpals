using System;

namespace SAP.Models
{
    public class Comic
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Commentary { get; set; }
        public DateTime Date { get; set; }
        public int EpisodeNumber { get; set; }
        public int? EpisodeSubNumber { get; set; }
        public string Chapter { get; set; }
        public bool IsAnimated { get; set; }
        public Comic()
        {
        }

    }
}
