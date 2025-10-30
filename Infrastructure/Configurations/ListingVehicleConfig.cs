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
    public class ListingVehicleConfig : IEntityTypeConfiguration<ListingVehicle>
    {
        public void Configure(EntityTypeBuilder<ListingVehicle> builder)
        {
            builder.HasOne(lv => lv.Listing)
                   .WithMany(l => l.ListingVehicles)
                   .HasForeignKey(lv => lv.ListingId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(lv => lv.Vehicle)
                   .WithMany(v => v.ListingVehicles)
                   .HasForeignKey(lv => lv.VehicleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
