using IG.Domain;
using Microsoft.EntityFrameworkCore;

namespace IG.Data
{
    public class IgContext: DbContext
    {
        public DbSet<HierarchyNode> HierarchyNodes { get; set; }
        public DbSet<TimeFrames> TimeFrames { get; set; }
        public DbSet<HierarchyMarket> HierarchyMarkets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=IG; Trusted_Connection=true");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HierarchyMarket>()
                .HasIndex(u => u.HierarchyNodeId)
                .IsUnique();

            //builder.Entity<HierarchyMarket>(entity => {
            //    entity.HasIndex(e => e.HierarchyNodeId).IsUnique();
            //});
        }


    }
}