using BikeSharingApp.Data;
using BikeSharingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeSharingApp.Repositories
{
    public class BikesRepository : IBikesRepository
    {
        private readonly ApplicationDbContext _context;

        public BikesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bike>> GetAllBikesAsync()
        {
            return await _context.Bikes.ToListAsync();
        }
        public async Task<Bike> GetBikeByIdAsync(int id)
        {
            return await _context.Bikes
                .Include(b => b.Location)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _context.Locations.ToListAsync();
        }
        public async Task<bool> AddBikeAsync(Bike bike)
        {
            _context.Bikes.Add(bike);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task UpdateBikeAsync(Bike bike)
        {
            _context.Bikes.Update(bike);
            await _context.SaveChangesAsync();
        }
    }
}
