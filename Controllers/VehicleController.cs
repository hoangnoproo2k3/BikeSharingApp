using Microsoft.AspNetCore.Mvc;
using BikeSharingApp.Models;
using System.Linq;
using BikeSharingApp.Data;
using BikeSharingApp.Utils;

namespace BikeSharingApp.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Search(int? location)
        {
            var viewModel = new SearchViewModel();

            if (location.HasValue)
            {
                viewModel.Bikes = _context.Bikes
                    .Where(b => b.LocationId == location.Value)
                    .ToList();
                var locationEntity = _context.Locations
                .FirstOrDefault(l => l.Id == location.Value);

                viewModel.LocationName = locationEntity?.Name;
            }
            else
            {
                viewModel.Bikes = _context.Bikes.ToList();
            }

            viewModel.Locations = _context.Locations.ToList();

            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            var viewModel = new DetailBikeViewModel();

            var bike = _context.Bikes.FirstOrDefault(b => b.Id == id);
            if (bike == null)
            {
                return NotFound();
            }
            viewModel.Bike = bike;
            viewModel.BikeName = bike.BikeName;
            var user = HttpContext.Session.Get<User>("User");
            if (user != null && bike.OwnerId == user.Id)
            {
                viewModel.Check = false;
            }
            else
            {
                viewModel.Check = true;
            }
            return View(viewModel);
        }
    }
}
