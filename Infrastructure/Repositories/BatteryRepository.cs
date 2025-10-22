using Application.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BatteryRepository : GenericRepository<Battery>, IBatteryRepository
    {
        public BatteryRepository(AppDbContext context) : base(context)
        {
        }

        public Task<List<Battery>> GetTrendingBatteriesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
