using Microsoft.EntityFrameworkCore;
using BikeSharingApp.Models;

namespace BikeSharingApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Status> Statuses { get; set; }
        // public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình các mối quan hệ và các ràng buộc khác nếu cần
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Bike>()
                .HasOne(b => b.Owner)
                .WithMany(u => u.OwnedBikes)
                .HasForeignKey(b => b.OwnerId);

            modelBuilder.Entity<Bike>()
                .HasOne(b => b.Location)
                .WithMany(l => l.Bikes)
                .HasForeignKey(b => b.LocationId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Bike)
                .WithMany(bk => bk.Bookings)
                .HasForeignKey(b => b.BikeId);

            modelBuilder.Entity<Booking>()
                          .HasOne(b => b.Status)
                          .WithMany()
                          .HasForeignKey(b => b.StatusId);
            // modelBuilder.Entity<Review>()
            //     .HasOne(r => r.Booking)
            //     .WithMany(b => b.Reviews)
            //     .HasForeignKey(r => r.BookingId);
            modelBuilder.Entity<Location>().HasData(
                new Location
                {
                    Id = 1,
                    Name = "Công viên Thống Nhất",
                    Address = "Tôn Thất Tùng, Đống Đa, Hà Nội",
                    Coordinates = "21.0081,105.5204"
                },
                new Location
                {
                    Id = 2,
                    Name = "Công viên Hoàn Kiếm",
                    Address = "Hồ Hoàn Kiếm, Hoàn Kiếm, Hà Nội",
                    Coordinates = "21.0285,105.8542"
                },
                new Location
                {
                    Id = 3,
                    Name = "Công viên Cầu Giấy",
                    Address = "Cầu Giấy, Hà Nội",
                    Coordinates = "21.0291,105.7795"
                },
                new Location
                {
                    Id = 4,
                    Name = "Công viên Yên Sở",
                    Address = "Yên Sở, Hoàng Mai, Hà Nội",
                    Coordinates = "20.9915,105.8235"
                },
                new Location
                {
                    Id = 5,
                    Name = "Công viên Bắc Linh Đàm",
                    Address = "Linh Đàm, Hoàng Mai, Hà Nội",
                    Coordinates = "20.9955,105.8177"
                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin", Description = "Administrator" },
                new Role { Id = 2, RoleName = "User", Description = "Regular user" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "Approved" },
                new Status { Id = 3, Name = "Rejected" }
            );
        }
    }
}