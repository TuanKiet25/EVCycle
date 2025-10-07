using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class ListingConfig : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            builder.HasKey(l => l.Id);
            builder.HasMany(l => l.Vehicles)
                   .WithOne(v => v.Listing)
                   .HasForeignKey(v => v.ListingId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(l => l.Batteries)
                     .WithOne(b => b.Listing)
                     .HasForeignKey(b => b.ListingId)
                     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
