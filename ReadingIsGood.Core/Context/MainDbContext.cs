using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Core.Context.Mappings;
using ReadingIsGood.Domain;

namespace ReadingIsGood.Core.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(CustomerMappings.OnModelCreating);
            builder.Entity<Order>(OrderMappings.OnModelCreating);
            builder.Entity<Stock>(StockMappings.OnModelCreating);
            builder.Entity<User>(UserMappigns.OnModelCreating);
        }
    }
}
