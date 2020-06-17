using System;

namespace Sap.Models
{
    public class Comic
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Commentary { get; set; }
        public DateTime Date { get; set; }
        public int EpisodeNumber { get; set; }
        public string Chapter { get; set; }

        public Comic()
        {
        }

    }
}
