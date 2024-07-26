using Microsoft.AspNetCore.Mvc;
using BikeSharingApp.Data;
using BikeSharingApp.Models;
using Microsoft.EntityFrameworkCore;
using BikeSharingApp.Repositories;

namespace BikeSharingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> ListUsers()
        {
            var users = await _repository.GetAllUsersAsync();
            return View(users);
        }

    }
}