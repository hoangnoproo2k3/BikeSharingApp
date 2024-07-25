using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class Bike
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string BikeName { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public decimal Price { get; set; }

        public int OwnerId { get; set; }

        [StringLength(20)]
        public string OwnerPhone { get; set; }

        [StringLength(20)]
        public string BikeStatus { get; set; }

        public string Description { get; set; }

        [StringLength(255)]
        public string Img { get; set; }

        public int LocationId { get; set; }

        public decimal PricePerHour { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public User Owner { get; set; }
        public Location Location { get; set; }
    }
}