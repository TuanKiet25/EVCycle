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
    public class BatteryCompatibilityConfig : IEntityTypeConfiguration<BatteryCompatibility>
    {
        public void Configure(EntityTypeBuilder<BatteryCompatibility> builder)
        {
            builder.HasOne(bc => bc.Battery)
                   .WithMany(b => b.BatteryCompatibilities)
                   .HasForeignKey(bc => bc.BatteryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(bc => bc.Vehicle)
                   .WithMany(v => v.BatteryCompatibilities)
                   .HasForeignKey(bc => bc.VehicleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
