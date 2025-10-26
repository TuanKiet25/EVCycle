using Application.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BatteryCompatibilityRepository : GenericRepository<BatteryCompatibility>, IBatteryCompatibilityRepository
    {
        public BatteryCompatibilityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
