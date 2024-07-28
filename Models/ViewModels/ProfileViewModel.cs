using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class ProfileViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
        public IEnumerable<Bike> CreatedBikes { get; set; }
        public IEnumerable<ReservedBikesModel> ReservedBikes { get; set; }
    }
    public class ReservedBikesModel
    {
        public Bike Bike { get; set; }
        public Booking Booking { get; set; }
        public Status Status { get; set; }
    }
}