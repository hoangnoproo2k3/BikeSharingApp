using Microsoft.AspNetCore.Mvc;
using BikeSharingApp.Data;
using BikeSharingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeSharingApp.Controllers
{
    public class DataSeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataSeedController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult SeedData()
        {
            // Các phương thức tạo dữ liệu sẽ được gọi ở đây
            CreateRoles();
            CreateUsers();
            CreateLocations();
            CreateBikes();
            CreateBookings();
            // CreateReviews();

            return Content("Data seeded successfully");
        }

        private void CreateRoles()
        {
            var roles = new List<Role>
            {
                new Role { RoleName = "Admin", Description = "Administrator" },
                new Role { RoleName = "User", Description = "Regular user" }
            };

            _context.Roles.AddRange(roles);
            _context.SaveChanges();
        }

        private void CreateUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    FullName = "John Doe",
                    Email = "john@example.com",
                    Phone = "1234567890",
                    RoleId = 1, // Admin role
                    Password = "hashedpassword", // Remember to hash passwords in real applications
                    CreatedDate = DateTime.Now
                },
                new User
                {
                    FullName = "Jane Smith",
                    Email = "jane@example.com",
                    Phone = "0987654321",
                    RoleId = 2, // User role
                    Password = "hashedpassword",
                    CreatedDate = DateTime.Now
                }
            };

            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        private void CreateLocations()
        {
            var locations = new List<Location>
            {
                new Location
                {
                    Name = "City Center",
                    Address = "123 Main St, City",
                    Coordinates = "40.7128,-74.0060"
                },
                new Location
                {
                    Name = "Suburb Station",
                    Address = "456 Park Ave, Suburb",
                    Coordinates = "40.7282,-73.7949"
                }
            };

            _context.Locations.AddRange(locations);
            _context.SaveChanges();
        }

        private void CreateBikes()
        {
            var bikes = new List<Bike>
            {
                new Bike
                {
                    BikeName = "Mountain Bike",
                    Price = 100.00m,
                    OwnerId = 1,
                    Description = "Great mountain bike for off-road adventures",
                    LocationId = 1,
                    PricePerHour = 5.00m,
                },
                new Bike
                {
                    BikeName = "City Cruiser",
                    Price = 80.00m,
                    OwnerId = 2,
                    Description = "Comfortable bike for city rides",
                    LocationId = 2,
                    PricePerHour = 4.00m,
                }
            };

            _context.Bikes.AddRange(bikes);
            _context.SaveChanges();
        }

        private void CreateBookings()
        {
            var bookings = new List<Booking>
            {
                new Booking
                {
                    CustomerId = 2,
                    BikeId = 1,
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(2),
                    TotalPrice = 24.00m,
                    Status = "Confirmed"
                }
            };

            _context.Bookings.AddRange(bookings);
            _context.SaveChanges();
        }

    }

}