using IG.Domain;
using Microsoft.EntityFrameworkCore;

namespace IG.Data
{
    public class IgContext: DbContext
    {
        public DbSet<HierarchyNode> HierarchyNodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=IG; Trusted_Connection=true");
        }


    }
}