using Microsoft.EntityFrameworkCore;
using Sap.API.EF.EntityFramework.Models;

namespace Sap.API.EF.EntityFramework.Context
{

    public class ChapterContext : DbContext
    {
        public ChapterContext(DbContextOptions<ChapterContext> options)
            : base(options)
        { }

        public DbSet<ChapterEf> Chapters { get; set; }
    }
}
