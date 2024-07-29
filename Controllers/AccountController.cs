
using BikeSharingApp.Data;
using BikeSharingApp.Models;
using BikeSharingApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeSharingApp.Controllers
{

    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại trong cơ sở dữ liệu chưa
                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email already in use.");
                    return View(model);
                }

                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Password = model.Password,
                    CreatedDate = DateTime.Now
                };

                try
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.Set("User", user);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating your account.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    HttpContext.Session.Set("User", user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Profile()
        {
            var user = HttpContext.Session.Get<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var statuses = _context.Statuses.ToList();
            var bikes = _context.Bikes.ToList();

            // Lấy danh sách xe đã đặt bởi người dùng thông qua Booking
            var ReservedBookingBikes = _context.Bookings.Where(b => b.CustomerId == user.Id).ToList();

            var model = new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                ReservedBookingBikes = ReservedBookingBikes
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult Management_Post()
        {
            var user = HttpContext.Session.Get<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // Lấy danh sách xe đã tạo bởi người dùng
            var createdBikes = _context.Bikes.Where(b => b.OwnerId == user.Id).ToList();
            var locations = _context.Locations.ToList();

            var model = new ProfileViewModel
            {
                CreatedBikes = createdBikes,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
        {
            var user = HttpContext.Session.Get<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userToUpdate = await _context.Users.FindAsync(user.Id);
            if (userToUpdate != null)
            {
                userToUpdate.FullName = model.FullName;
                userToUpdate.Email = model.Email;
                userToUpdate.Phone = model.Phone;

                _context.Users.Update(userToUpdate);
                await _context.SaveChangesAsync();

                // Cập nhật thông tin người dùng trong session
                HttpContext.Session.Set("User", userToUpdate);
                TempData["SuccessMessage"] = "Profile updated successfully.";
                return RedirectToAction("Profile");
            }

            return View("Profile", model);
        }
    }

}