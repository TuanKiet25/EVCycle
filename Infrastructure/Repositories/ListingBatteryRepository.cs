using Application.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ListingBatteryRepository : GenericRepository<ListingBattery>, IListingBatteryRepository
    {
        public ListingBatteryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
