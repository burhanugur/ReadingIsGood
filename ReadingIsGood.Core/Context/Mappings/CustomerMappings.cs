using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingIsGood.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingIsGood.Core.Context.Mappings
{
    public static class CustomerMappings
    {
        public static void OnModelCreating(EntityTypeBuilder<Customer> builder)
        {
            builder.HasOne(x => x.User).WithOne(x => x.Customer);
        }
    }
}
