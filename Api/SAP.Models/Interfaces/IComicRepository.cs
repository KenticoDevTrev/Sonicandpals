using SAP.Models.SaP;
using System;
using System.Collections.Generic;

namespace SAP.Models.Interfaces
{
    public interface IComicRepository
    {
        IEnumerable<Comic> GetTodaysComics();
        IEnumerable<Comic> GetComics(ComicQuery Query);
        /// <summary>
        /// Votes for the given episode.
        /// </summary>
        /// <param name="EpisodeNumber">The Episode Number</param>
        /// <param name="EpisodeSubNumber">Optionally the Sub Number</param>
        /// <param name="Rating">The Star Rating (1-5)</param>
        /// <param name="IPAddress">The User's IP Address</param>
        /// <returns>True if vote successful, false if already voted.</returns>
        bool Vote(int EpisodeNumber, int? EpisodeSubNumber, int Rating, string IPAddress);
    }
}