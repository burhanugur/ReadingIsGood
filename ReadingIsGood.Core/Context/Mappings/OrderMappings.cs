using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingIsGood.Domain;

namespace ReadingIsGood.Core.Context.Mappings
{
    public static class OrderMappings
    {
        public static void OnModelCreating(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(x => x.Customer).WithMany(x => x.Orders).HasForeignKey(x => x.CustomerId);
        }
    }
}
