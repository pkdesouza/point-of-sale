using System.Configuration;
using Microsoft.EntityFrameworkCore;
using PointOfSaleInfra.Entities;

namespace PointOfSaleInfra
{
    public partial class PointOfSaleContext : DbContext
    {
        public PointOfSaleContext(DbContextOptions<PointOfSaleContext> options) : base(options)
        {
        }

        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Coin> Coin { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["ConnectionString"]);
            }
        }
    }
}
