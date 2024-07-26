using Microsoft.AspNetCore.Mvc;
using BikeSharingApp.Data;
using BikeSharingApp.Models;
using Microsoft.EntityFrameworkCore;
using BikeSharingApp.Repositories;

namespace BikeSharingApp.Controllers
{
    public class BikesController : Controller
    {
        private readonly IBikesRepository _repository;

        public BikesController(IBikesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> ListBikes()
        {
            var bikes = await _repository.GetAllBikesAsync();
            return View(bikes);
        }

    }
}