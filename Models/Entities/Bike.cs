using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public string BikeName { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int? OwnerId { get; set; }
        public string OwnerPhone { get; set; }
        public string BikeStatus { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int? LocationId { get; set; }
        public decimal PricePerHour { get; set; }
        public string Status { get; set; }

        public User Owner { get; set; }
        public Location Location { get; set; }
        public ICollection<Booking> Bookings { get; set; }

        // [Required]
        // [StringLength(10, MinimumLength = 10)]
        // [RegularExpression(@"^\d.*$", ErrorMessage = "VerifyKey must start with a digit and be exactly 10 characters long.")]
        // public string VerifyKey { get; set; }
    }
}