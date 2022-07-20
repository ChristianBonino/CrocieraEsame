using Microsoft.EntityFrameworkCore;
using Crociera.DB.Entities;

namespace Crociera.DB
{
    public class CrocieraDBContext : DbContext
    {
        public CrocieraDBContext(DbContextOptions<CrocieraDBContext> options)
            : base(options)
        {
        }
        public DbSet<Prenotazioni> Prenotazioni { get; set; }
        public DbSet<Eventi> Eventi { get; set; }
        public DbSet<Locali> Locali { get; set; }
        public DbSet<Repliche> Repliche { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}