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
        public async Task<IActionResult> Pending(int? bikeId)
        {
            var userId = HttpContext.Session.Get<User>("User")?.Id;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách các xe do người dùng tạo và danh sách địa điểm chỉ một lần
            var createdBikes = await _context.Bikes
                .Where(b => b.OwnerId == userId)
                .OrderByDescending(b => b.Id)
                .ToListAsync();
            // Lấy danh sách các yêu cầu phê duyệt dựa trên bikeId (nếu có)
            var bookingsQuery = _context.Bookings
                .Include(b => b.Bike)
                .Include(b => b.Customer)
                .Include(b => b.Status)
                .Where(b => b.Bike.OwnerId == userId);

            if (bikeId.HasValue)
            {
                bookingsQuery = bookingsQuery.Where(b => b.Bike.Id == bikeId.Value);
            }

            var pendingBookings = await bookingsQuery
                .Select(b => new PendingBookingModel
                {
                    Booking = b,
                    Bike = b.Bike,
                    Customer = b.Customer,
                    Status = b.Status
                })
                .ToListAsync();

            var bikeName = bikeId.HasValue
                ? await _context.Bikes
                    .Where(b => b.Id == bikeId.Value)
                    .Select(b => b.BikeName)
                    .SingleOrDefaultAsync()
                : null;

            var viewModel = new PendingBookingsViewModel
            {
                CreatedBikes = createdBikes,
                PendingBookings = pendingBookings,
                BikeName = bikeName
            };

            return View(viewModel);
        }
        public async Task<IActionResult> GetPendingBookings(int? bikeId)
        {
            var userId = HttpContext.Session.Get<User>("User")?.Id;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var query = _context.Bookings
                .Include(b => b.Bike)
                .Include(b => b.Customer)
                .Include(b => b.Status)
                .Where(b => b.Bike.OwnerId == userId)
                .AsQueryable();

            if (bikeId.HasValue)
            {
                query = query.Where(b => b.Bike.Id == bikeId.Value);
            }

            var pendingBookings = await query.ToListAsync();

            var bikeName = bikeId.HasValue
                ? await _context.Bikes.Where(b => b.Id == bikeId).Select(b => b.BikeName).FirstOrDefaultAsync()
                : null;

            var result = new
            {
                pendingBookings = pendingBookings.Select(b => new
                {
                    booking = new { id = b.Id },
                    bike = new { bikeName = b.Bike.BikeName },
                    customer = new { fullName = b.Customer.FullName },
                    status = new { id = b.Status.Id, name = b.Status.Name }
                }),
                bikeName
            };

            return Json(result);
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