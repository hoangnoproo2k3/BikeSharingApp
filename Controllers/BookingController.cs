using BikeSharingApp.Data;
using BikeSharingApp.Models;
using BikeSharingApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeSharingApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Booking/Pending
        public IActionResult Pending(int? bikeId)
        {
            var userId = HttpContext.Session.Get<User>("User")?.Id;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách các xe do người dùng tạo
            var createdBikes = _context.Bikes.Where(b => b.OwnerId == userId).OrderByDescending(b => b.Id).ToList();
            var locations = _context.Locations.ToList();
            var viewModel = new PendingBookingsViewModel();

            // Nếu có bikeId, thêm điều kiện lọc theo bikeId
            if (bikeId.HasValue)
            {
                viewModel.PendingBookings = _context.Bookings
                    .Include(b => b.Bike)
                    .Include(b => b.Customer)
                    .Include(b => b.Status)
                    .Where(b => b.Bike.OwnerId == userId && b.Bike.Id == bikeId.Value)
                    .Select(b => new PendingBookingModel
                    {
                        Booking = b,
                        Bike = b.Bike,
                        Customer = b.Customer,
                        Status = b.Status
                    })
                    .ToList();
                var bikeEntity = _context.Bikes
                .FirstOrDefault(l => l.Id == bikeId.Value);
                viewModel.BikeName = bikeEntity?.BikeName;
            }
            else
            {
                viewModel.PendingBookings = _context.Bookings
                    .Include(b => b.Bike)
                    .Include(b => b.Customer)
                    .Include(b => b.Status)
                    .Where(b => b.Bike.OwnerId == userId)
                    .Select(b => new PendingBookingModel
                    {
                        Booking = b,
                        Bike = b.Bike,
                        Customer = b.Customer,
                        Status = b.Status
                    })
                    .ToList();
            }

            viewModel.CreatedBikes = createdBikes;

            return View(viewModel);
        }
        public IActionResult Approve(int id)
        {
            var booking = _context.Bookings.Include(b => b.Bike).FirstOrDefault(b => b.Id == id);
            if (booking == null || booking.Bike.OwnerId != HttpContext.Session.Get<User>("User")?.Id)
            {
                return NotFound();
            }
            // Check if already approved or rejected
            if (booking.StatusId != 1)
            {
                return RedirectToAction("Pending");
            }

            booking.StatusId = 2; // Assuming 2 is the ID for "Approved"
            _context.Update(booking);
            _context.SaveChanges();
            return RedirectToAction("Pending");
        }
        public IActionResult Reject(int id)
        {
            var booking = _context.Bookings.Include(b => b.Bike).FirstOrDefault(b => b.Id == id);
            if (booking == null || booking.Bike.OwnerId != HttpContext.Session.Get<User>("User")?.Id)
            {
                return NotFound();
            }

            // Check if already approved or rejected
            if (booking.StatusId != 1)
            {
                return RedirectToAction("Pending");
            }

            booking.StatusId = 3; // Assuming 3 is the ID for "Rejected"
            _context.Update(booking);
            _context.SaveChanges();
            return RedirectToAction("Pending");
        }
    }

}