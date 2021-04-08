using CMS.DataEngine;
using CMS.Helpers;
using SAP.Models;
using SAP.Models.Interfaces;
using SAP.Models.SaP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SAP.Library.Implementations
{
    public class ComicRepository : IComicRepository
    {
        public ComicRepository(IEpisodeInfoProvider episodeInfoProvider, IEpisodeRatingInfoProvider episodeRatingInfoProvider)
        {
            EpisodeInfoProvider = episodeInfoProvider;
            EpisodeRatingInfoProvider = episodeRatingInfoProvider;
        }

        public IEpisodeInfoProvider EpisodeInfoProvider { get; }
        public IEpisodeRatingInfoProvider EpisodeRatingInfoProvider { get; }

        public IEnumerable<Comic> GetComics(ComicQuery Query)
        {
            return CacheHelper.Cache(cs =>
            {
                bool ByEpisode = false;
                var EpisodeQuery = EpisodeInfoProvider.Get()
                    .Source(x => x.Join<ChapterInfo>(nameof(EpisodeInfo.EpisodeChapterID), nameof(ChapterInfo.ChapterID)))
                    .Source(x => x.LeftJoin(new QuerySourceTable("SAP_EpisodeRating"), $"SAP_Episode.{nameof(EpisodeInfo.EpisodeID)}", nameof(EpisodeRatingInfo.EpisodeRatingEpisodeID)))
                    .OrderBy(nameof(EpisodeInfo.EpisodeNumber), nameof(EpisodeInfo.EpisodeSubNumber));
                if (Query.EpisodeNumber > 0)
                {
                    ByEpisode = true;
                    EpisodeQuery.WhereGreaterOrEquals(nameof(EpisodeInfo.EpisodeNumber), Query.EpisodeNumber);
                }
                else if (Query.Date != DateTimeHelper.ZERO_TIME)
                {
                    ByEpisode = false;
                    EpisodeQuery.WhereGreaterOrEquals(nameof(EpisodeInfo.EpisodeDate), Query.Date.Date);
                }
                List<string> GroupByColumns = new List<string>()
                {
                    nameof(EpisodeInfo.EpisodeTitle),
                    nameof(EpisodeInfo.EpisodeNumber),
                    nameof(EpisodeInfo.EpisodeSubNumber),
                    nameof(EpisodeInfo.EpisodeIsAnimation),
                    nameof(EpisodeInfo.EpisodeFileUrl),
                    nameof(ChapterInfo.ChapterTitle),
                    nameof(EpisodeInfo.EpisodeDate)
                };
                
                if (Query.IncludeCommentary)
                {
                    GroupByColumns.Add(nameof(EpisodeInfo.EpisodeCommentary));
                }
                // Daily or Weekly limit
                if (Query.Type.Equals("weekly"))
                {
                    if (ByEpisode)
                    {
                        EpisodeQuery.WhereLessThan(nameof(EpisodeInfo.EpisodeNumber), Query.EpisodeNumber + 7);
                    }
                    else
                    {
                        EpisodeQuery.WhereLessThan(nameof(EpisodeInfo.EpisodeDate), Query.Date.AddDays(7).Date);
                    }
                }
                else
                {
                    if (ByEpisode)
                    {
                        EpisodeQuery.WhereLessThan(nameof(EpisodeInfo.EpisodeNumber), Query.EpisodeNumber + 1);
                    }
                    else
                    {
                        EpisodeQuery.WhereLessThan(nameof(EpisodeInfo.EpisodeDate), Query.Date.AddDays(1).Date);
                    }
                }

                List<string> Columns = new List<string>(GroupByColumns);
                Columns.Add("AVG(cast(COALESCE(EpisodeRatingValue, 0) as float)) as EpisodeRatingValue");

                EpisodeQuery
                .Columns(Columns)
                .GroupBy(GroupByColumns.ToArray());

                var Episodes = EpisodeQuery.Result.Tables[0].Rows.Cast<DataRow>().Select(dr =>
                {
                    var Episode = new EpisodeInfo(dr);
                    return new Comic()
                    {
                        Date = Episode.EpisodeDate,
                        Chapter = (string)dr[nameof(ChapterInfo.ChapterTitle)],
                        Commentary = Episode.EpisodeCommentary,
                        EpisodeNumber = Episode.EpisodeNumber,
                        EpisodeSubNumber = (Episode.GetValue("EpisodeSubNumber") == null ? (int?)null : Episode.GetIntegerValue("EpisodeSubNumber", 0)),
                        ImageUrl = Episode.EpisodeFileUrl,
                        Title = Episode.EpisodeTitle,
                        IsAnimated = Episode.EpisodeIsAnimation,
                        AverageRating = ValidationHelper.GetDouble(dr[nameof(EpisodeRatingInfo.EpisodeRatingValue)], 0)
                    };
                }).ToList();

                if (cs.Cached)
                {
                    cs.CacheDependency = CacheHelper.GetCacheDependency(Episodes.Select(x => $"VoteOn|{x.EpisodeNumber}").ToArray());
                }

                return Episodes;
            }, new CacheSettings(30, "GetComics|" + Query.ToString()));
        }

        public IEnumerable<Comic> GetTodaysComics()
        {
            return CacheHelper.Cache(cs =>
            {

                List<string> GroupByColumns = new List<string>()
                {
                    nameof(EpisodeInfo.EpisodeTitle),
                    nameof(EpisodeInfo.EpisodeNumber),
                    nameof(EpisodeInfo.EpisodeSubNumber),
                    nameof(EpisodeInfo.EpisodeIsAnimation),
                    nameof(EpisodeInfo.EpisodeFileUrl),
                    nameof(ChapterInfo.ChapterTitle),
                    nameof(EpisodeInfo.EpisodeCommentary),
                    nameof(EpisodeInfo.EpisodeDate)
                };
                List<string> Columns = new List<string>(GroupByColumns);
                Columns.Add("AVG(cast(COALESCE(EpisodeRatingValue, 0) as float)) as EpisodeRatingValue");
                // Launch Date
                DateTime LaunchDate = new DateTime(2021, 4, 1);
                int EpisodeNumber = Convert.ToInt32((DateTime.Now - LaunchDate).TotalDays % 2786) + 1;
                var Query = EpisodeInfoProvider.Get()
                    .WhereEquals(nameof(EpisodeInfo.EpisodeNumber), EpisodeNumber)
                    .Source(x => x.Join<ChapterInfo>(nameof(EpisodeInfo.EpisodeChapterID), nameof(ChapterInfo.ChapterID)))
                    .Source(x => x.LeftJoin(new QuerySourceTable("SAP_EpisodeRating"), $"SAP_Episode.{nameof(EpisodeInfo.EpisodeID)}", nameof(EpisodeRatingInfo.EpisodeRatingEpisodeID)))
                    .Columns(Columns)
                    .GroupBy(GroupByColumns.ToArray())
                    .OrderBy(nameof(EpisodeInfo.EpisodeSubNumber));
                var Episodes = Query.Result.Tables[0].Rows.Cast<DataRow>().Select(dr =>
                {
                    var Episode = new EpisodeInfo(dr);
                    return new Comic()
                    {
                        Date = Episode.EpisodeDate,
                        Chapter = (string)dr[nameof(ChapterInfo.ChapterTitle)],
                        Commentary = Episode.EpisodeCommentary,
                        EpisodeNumber = Episode.EpisodeNumber,
                        EpisodeSubNumber = (Episode.GetValue("EpisodeSubNumber") == null ? (int?)null : Episode.GetIntegerValue("EpisodeSubNumber", 0)),
                        ImageUrl = Episode.EpisodeFileUrl,
                        Title = Episode.EpisodeTitle,
                        IsAnimated = Episode.EpisodeIsAnimation,
                        AverageRating = ValidationHelper.GetDouble(dr[nameof(EpisodeRatingInfo.EpisodeRatingValue)], 0)
                    };
                }).ToList();

                if (cs.Cached)
                {
                    cs.CacheDependency = CacheHelper.GetCacheDependency(Episodes.Select(x => $"VoteOn|{x.EpisodeNumber}").ToArray());
                }

                return Episodes;
            }, new CacheSettings(1440, "GetTodaysComic", DateTime.Now.Date.ToString()));
        }

        public bool Vote(int EpisodeNumber, int? EpisodeSubNumber, int Rating, string IPAddress)
        {
            // Invalid range
            if(Rating < 1 || Rating > 5)
            {
                return false;
            }

            // Get person's last rating, if it was within 5 seconds, block
            var LastVote = EpisodeRatingInfoProvider.Get()
                .WhereEquals(nameof(EpisodeRatingInfo.EpisodeRatingIP), IPAddress)
                .Where($"Datediff(second, {nameof(EpisodeRatingInfo.EpisodeRatingLastModified)}, GETDATE()) < 5")
                .TopN(1) 
                .OrderByDescending(nameof(EpisodeRatingInfo.EpisodeRatingLastModified))
                .FirstOrDefault();
            if(LastVote != null)
            {
                return false;
            }

            // Get EpisodeID
            var EpisodeObj = EpisodeInfoProvider.Get()
                .WhereEquals(nameof(EpisodeInfo.EpisodeNumber), EpisodeNumber)
                .WhereEqualsOrNull(nameof(EpisodeInfo.EpisodeSubNumber), EpisodeSubNumber ?? 0)
                .Columns(nameof(EpisodeInfo.EpisodeID))
                .FirstOrDefault();

            if (EpisodeObj == null)
            {
                return false;
            }

            var existingRating = EpisodeRatingInfoProvider.Get()
                .WhereEquals(nameof(EpisodeRatingInfo.EpisodeRatingEpisodeID), EpisodeObj.EpisodeID)
                .WhereEquals(nameof(EpisodeRatingInfo.EpisodeRatingIP), IPAddress)
                .FirstOrDefault();
            if (existingRating != null)
            {
                return false;
            }

            EpisodeRatingInfoProvider.Set(new EpisodeRatingInfo()
            {
                EpisodeRatingEpisodeID = EpisodeObj.EpisodeID,
                EpisodeRatingIP = IPAddress,
                EpisodeRatingValue = Rating
            });

            // Trigger dependency
            CacheHelper.TouchKey($"VoteOn|{EpisodeNumber}");

            return true;
        }
    }
}
