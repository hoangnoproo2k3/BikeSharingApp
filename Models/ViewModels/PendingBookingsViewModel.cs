using System.ComponentModel.DataAnnotations;
namespace BikeSharingApp.Models
{
    public class PendingBookingsViewModel
    {
        public IEnumerable<PendingBookingModel> PendingBookings { get; set; }
        public string BikeName { get; set; }
        public IEnumerable<Bike> CreatedBikes { get; set; }
    }

    public class PendingBookingModel
    {
        public Booking Booking { get; set; }
        public Bike Bike { get; set; }
        public User Customer { get; set; }
        public Status Status { get; set; }
    }

}
