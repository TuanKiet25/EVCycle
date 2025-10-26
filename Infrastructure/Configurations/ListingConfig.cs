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
            builder.HasOne(l => l.Vehicle)
                     .WithMany(v => v.Listings)
                     .HasForeignKey(l => l.VehicleId)
                     .IsRequired(false)
                     .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(l => l.Battery)
                        .WithMany(b => b.Listings)
                        .HasForeignKey(l => l.BatteryId)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
