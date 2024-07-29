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
            var location = _context.Locations.FirstOrDefault(b => b.Id == bike.LocationId);
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
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null)
            {
                return NotFound();
            }

            _context.Bikes.Remove(bike);
            await _context.SaveChangesAsync();

            return RedirectToAction("Management_Post", "Account");
        }
        [HttpGet]
        public IActionResult Book(int? id)
        {
            var user = HttpContext.Session.Get<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new BookingViewModel();

            var bike = _context.Bikes.FirstOrDefault(b => b.Id == id);
            if (bike == null)
            {
                return NotFound();
            }
            viewModel.Bike = bike;
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Book(BookingViewModel model)
        {
            var user = HttpContext.Session.Get<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bike = _context.Bikes.Find(model.Bike.Id);
            if (bike == null)
            {
                return NotFound();
            }
            var totalHours = (model.EndDate - model.StartDate).TotalHours;
            var totalDays = Math.Ceiling(totalHours / 24);

            // Chuyển đổi giá trị từ double sang decimal
            decimal totalHoursDecimal = (decimal)totalHours;
            decimal totalDaysDecimal = (decimal)totalDays;

            decimal totalPrice;
            if (totalHours < 24)
            {
                totalPrice = totalHoursDecimal * bike.PricePerHour;
            }
            else
            {
                totalPrice = totalDaysDecimal * bike.Price;
            }
            // Kiểm tra xem có booking nào đang chờ xử lý cho cùng một xe và khách hàng không
            var existingBooking = _context.Bookings
                .FirstOrDefault(b => b.BikeId == model.Bike.Id && b.CustomerId == user.Id && b.StatusId == 1);

            if (existingBooking != null)
            {
                // Thông báo lỗi hoặc thông báo khác
                TempData["ErrorMessage"] = "Bạn đã có một booking đang chờ xử lý cho xe này.";
                model.Bike = bike;
                return View("Book", model);
            }
            var booking = new Booking
            {
                CustomerId = HttpContext.Session.Get<User>("User")?.Id,
                BikeId = model.Bike.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TotalPrice = totalPrice,
                StatusId = 1
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return RedirectToAction("Profile", "Account");
        }

    }
}
