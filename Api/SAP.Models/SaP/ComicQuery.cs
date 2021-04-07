using System;

namespace SAP.Models.SaP
{
    public class ComicQuery
    {
        public string Type { get; set; } = "daily"; // daily, weekly
        public bool IncludeCommentary { get; set; } = true;
        public int EpisodeNumber { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.MinValue;

        public override string ToString()
        {
            return $"{Type}|{IncludeCommentary}|{EpisodeNumber}|{Date}";
        }
    }
}
