using System.Collections.Generic;
using System.Threading.Tasks;
using BikeSharingApp.Models;

namespace BikeSharingApp.Repositories
{
    public interface IBikesRepository
    {
        Task<IEnumerable<Bike>> GetAllBikesAsync();
    }
}
