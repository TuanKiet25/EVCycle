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
    public class ListingBatteryConfig : IEntityTypeConfiguration<ListingBattery>
    {
        public void Configure(EntityTypeBuilder<ListingBattery> builder)
        {
            builder.HasOne(lb => lb.Listing)
                   .WithMany(l => l.ListingBatteries)
                   .HasForeignKey(lb => lb.ListingId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(lb => lb.Battery)
                   .WithMany(b => b.ListingBatteries)
                   .HasForeignKey(lb => lb.BatteryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
