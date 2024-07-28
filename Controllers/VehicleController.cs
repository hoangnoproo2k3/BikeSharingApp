using Microsoft.AspNetCore.Mvc;
using BikeSharingApp.Models;
using System.Linq;
using BikeSharingApp.Data;

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
    }
}
