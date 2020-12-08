using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PointOfSaleInfra.Entities;

namespace PointOfSaleInfra
{
    public partial class PointOfSaleContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public PointOfSaleContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PointOfSaleContext(DbContextOptions<PointOfSaleContext> options) : base(options)
        {
        }

        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionString").Value);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ChangeMessage).IsRequired().HasColumnName("ChangeMessage").HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Change).IsRequired().HasColumnName("Change");
                entity.Property(e => e.ValueToPay).IsRequired().HasColumnName("ValueToPay");
                entity.Property(e => e.TotalValue).IsRequired().HasColumnName("TotalValue");
                entity.Property(e => e.RegistrationDate).HasColumnName("RegistrationDate").HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            });
        }
    }
}
