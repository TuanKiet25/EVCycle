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
           builder.HasOne(l => l.User)
                  .WithMany(u => u.Listings)
                  .HasForeignKey(l => l.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
