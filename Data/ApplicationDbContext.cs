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

            // modelBuilder.Entity<Review>()
            //     .HasOne(r => r.Booking)
            //     .WithMany(b => b.Reviews)
            //     .HasForeignKey(r => r.BookingId);
        }
    }
}