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

    }
}
