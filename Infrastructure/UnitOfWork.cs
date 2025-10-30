using Application;
using Application.IRepositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext _context;
        public IUserRepository userRepository { get; }
        public IBatteryRepository batteryRepository { get; }
        public IVehicleRepository vehicleRepository { get; }
        public IListingRepository listingRepository { get; }
        public IBatteryCompatibilityRepository batteryCompatibilityRepository { get; }
        public IListingBatteryRepository listingBatteryRepository { get; }
        public IListingVehicleRepository listingVehicleRepository { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            userRepository = new UserRepository(_context);
            batteryRepository = new BatteryRepository(_context);
            vehicleRepository = new VehicleRepository(_context);
            listingRepository = new ListingRepository(_context);
            batteryCompatibilityRepository = new BatteryCompatibilityRepository(_context);
            listingBatteryRepository = new ListingBatteryRepository(_context);
            listingVehicleRepository = new ListingVehicleRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
