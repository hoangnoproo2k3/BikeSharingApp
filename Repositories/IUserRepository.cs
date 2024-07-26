using System.Collections.Generic;
using System.Threading.Tasks;
using BikeSharingApp.Models;

namespace BikeSharingApp.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

    }
}
