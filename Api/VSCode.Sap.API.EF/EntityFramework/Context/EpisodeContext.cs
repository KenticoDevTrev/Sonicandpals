using Microsoft.EntityFrameworkCore;
using Sap.API.EF.EntityFramework.Models;

namespace Sap.API.EF.EntityFramework.Context
{
    public class EpisodeContext : DbContext
    {
        public EpisodeContext(DbContextOptions<EpisodeContext> options)
            : base(options)
        { }

        public DbSet <EpisodeEf> Episodes { get; set; }
        public DbSet<ChapterEf> Chapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Link Chapter
            modelBuilder.Entity<EpisodeEf>()
                .HasOne(x => x.Chapter)
                .WithMany()
                .HasPrincipalKey(x => x.ChapterID)
                .HasForeignKey(x => x.EpisodeChapterID);
            
            modelBuilder.Entity<EpisodeEf>()
                .HasMany(x => x.Ratings)
                .WithOne(x => x.Episode)
                .HasPrincipalKey(x => x.EpisodeID)
                .HasForeignKey(x => x.EpisodeRatingEpisodeID);
            
        }
    }
}
