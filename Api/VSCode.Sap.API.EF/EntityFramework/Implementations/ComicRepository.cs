using Sap.API.EF.EntityFramework.Context;
using SAP.Models;
using SAP.Models.Interfaces;
using SAP.Models.SaP;
using System;
using System.Collections.Generic;
using System.Linq;
using Sap.API.EF.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Sap.API.EF.EntityFramework.Implementations
{
    public class ComicRepository : IComicRepository
    {
        public ComicRepository(EpisodeContext episodeContext)
        {
            EpisodeContext = episodeContext;
        }

        public EpisodeContext EpisodeContext { get; }

        public IEnumerable<Comic> GetComics(ComicQuery Query)
        {
            bool ByEpisode = false;
            List<EpisodeEf> Episodes = new List<EpisodeEf>();

            if (Query.EpisodeNumber > 0)
            {
                ByEpisode = true;
            }
            else if (Query.Date != new DateTime(0))
            {
                ByEpisode = false;
            }

            // Daily or Weekly limit
            if (Query.Type.Equals("weekly", StringComparison.InvariantCultureIgnoreCase))
            {
                if (ByEpisode)
                {
                    Episodes = EpisodeContext.Episodes
                .Include(x => x.Chapter)
                .Include(x => x.Ratings)
                .Where(x => x.EpisodeNumber >= Query.EpisodeNumber && x.EpisodeNumber < (Query.EpisodeNumber + 7))
                .OrderBy(x => x.EpisodeNumber).ThenBy(x => x.EpisodeSubNumber)
                .ToList();
                }
                else
                {
                    Episodes = EpisodeContext.Episodes
                .Include(x => x.Chapter)
                .Include(x => x.Ratings)
                .Where(x => x.EpisodeDate >= Query.Date.Date && x.EpisodeDate < Query.Date.AddDays(7).Date)
                .OrderBy(x => x.EpisodeNumber).ThenBy(x => x.EpisodeSubNumber)
                .ToList();
                }
            }
            else
            {
                if (ByEpisode)
                {
                    Episodes = EpisodeContext.Episodes
                .Include(x => x.Chapter)
                .Include(x => x.Ratings)
                .Where(x => x.EpisodeNumber >= Query.EpisodeNumber && x.EpisodeNumber < (Query.EpisodeNumber + 1))
                .OrderBy(x => x.EpisodeNumber).ThenBy(x => x.EpisodeSubNumber)
                .ToList();

                }
                else
                {
                    Episodes = EpisodeContext.Episodes
                .Include(x => x.Chapter)
                .Include(x => x.Ratings)
                .Where(x => x.EpisodeDate >= Query.Date.Date && x.EpisodeDate < Query.Date.AddDays(1).Date)
                .OrderBy(x => x.EpisodeNumber).ThenBy(x => x.EpisodeSubNumber)
                .ToList();

                }
            }
            

            return Episodes
                .Select(x => new Comic()
                {
                    Date = x.EpisodeDate,
                    Chapter = x.Chapter.ChapterTitle,
                    Commentary = (Query.IncludeCommentary ? x.EpisodeCommentary : null),
                    EpisodeNumber = x.EpisodeNumber,
                    EpisodeSubNumber = x.EpisodeSubNumber,
                    ImageUrl = x.EpisodeFileUrl,
                    Title = x.EpisodeTitle,
                    IsAnimated = x.EpisodeIsAnimation,
                    AverageRating = (x.Ratings.Count > 0 ? x.Ratings.Average(x => x.EpisodeRatingValue) : 0)
                });
        }

        public IEnumerable<Comic> GetTodaysComics()
        {
            // Launch Date
            DateTime LaunchDate = new DateTime(2021, 6, 19);
            int EpisodeNumber = Convert.ToInt32((DateTime.Now - LaunchDate).TotalDays % 2786) + 1;
            var EpisodeQuery = EpisodeContext.Episodes
                .Include(x => x.Chapter)
                .Include(x => x.Ratings)
                .Where(x => x.EpisodeNumber == EpisodeNumber)
                .OrderBy(x => x.EpisodeNumber).ThenBy(x => x.EpisodeSubNumber);

            return EpisodeQuery.ToList()
                .Select(x => new Comic()
                {
                    Date = x.EpisodeDate,
                    Chapter = x.Chapter.ChapterTitle,
                    Commentary = x.EpisodeCommentary,
                    EpisodeNumber = x.EpisodeNumber,
                    EpisodeSubNumber = x.EpisodeSubNumber,
                    ImageUrl = x.EpisodeFileUrl,
                    Title = x.EpisodeTitle,
                    IsAnimated = x.EpisodeIsAnimation,
                    AverageRating = (x.Ratings.Count > 0 ? x.Ratings.Average(x => x.EpisodeRatingValue) : 0)
                });
        }

        public bool Vote(int EpisodeNumber, int? EpisodeSubNumber, int Rating, string IPAddress)
        {
            if (Rating <= 0 || Rating > 5)
            {
                return false;
            }
            EpisodeEf Episode;
            if(EpisodeSubNumber.HasValue)
            {
                Episode = EpisodeContext.Episodes
                .Include(x => x.Chapter)
                .Include(x => x.Ratings)
                .Where(x => x.EpisodeNumber == EpisodeNumber && x.EpisodeSubNumber == EpisodeSubNumber.Value)
                .FirstOrDefault();
            } else
            {
                Episode = EpisodeContext.Episodes
                .Include(x => x.Chapter)
                .Include(x => x.Ratings)
                .Where(x => x.EpisodeNumber == EpisodeNumber)
                .FirstOrDefault();
            }
            if (Episode == null)
            {
                return false;
            }
            if (!Episode.Ratings.Any(x => x.EpisodeRatingIP.Equals(IPAddress)))
            {
                var EpRating = new EpisodeRatingEf()
                {
                    EpisodeRatingIP = IPAddress,
                    EpisodeRatingLastModified = DateTime.Now,
                    EpisodeRatingValue = Rating,
                    EpisodeRatingEpisodeID = Episode.EpisodeID
                };
                EpisodeContext.Add(EpRating);
                EpisodeContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
