using Microsoft.EntityFrameworkCore;
using Sap.API.EF.EntityFramework.Models;

namespace Sap.API.EF.EntityFramework.Context
{
    public class InfoPageContext : DbContext
    {
        public InfoPageContext(DbContextOptions<InfoPageContext> options)
            : base(options)
        { }
        public DbSet<InfoPageEf> InfoPages { get; set; }

    }
}
