using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingIsGood.Domain;
using System;

namespace ReadingIsGood.Core.Context.Mappings
{
    public static class UserMappigns
    {
        public static void OnModelCreating(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User
            {
                Id = Guid.NewGuid(),
                Password = "a9JTmodZe2Q=",
                Username ="test"
            });
        }
    }
}
