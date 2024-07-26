using System.Collections.Generic;
using System.Threading.Tasks;
using BikeSharingApp.Models;

namespace BikeSharingApp.Repositories
{
    public interface IBikesRepository
    {
        Task<IEnumerable<Bike>> GetAllBikesAsync();
        Task<Bike> GetBikeByIdAsync(int id);
        Task<bool> AddBikeAsync(Bike bike);
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task UpdateBikeAsync(Bike bike);
    }
}
