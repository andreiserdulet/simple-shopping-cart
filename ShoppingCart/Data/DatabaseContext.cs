using Common.DatabaseSettings;
using Data.Models;
using Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {
            new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()});
        private readonly DbSettings _dbSettings;

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DatabaseContext(IOptions<DbSettings> optionSettings)
        {
            this._dbSettings = optionSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._dbSettings.ConnectionString);
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(x => x.Description).IsRequired().HasMaxLength(2000);
            modelBuilder.Entity<Product>().Property(x => x.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Image).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Category).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Product>().HasData(ProductInitialData.GetData());

            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Order>().Property(x => x.Email).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Order>().Property(x => x.PhoneNo).IsRequired().HasMaxLength(10);

            modelBuilder.Entity<Cart>().HasKey(x => x.Id);
            modelBuilder.Entity<Cart>().Property(x => x.Status).IsRequired();
        }
    }
}
