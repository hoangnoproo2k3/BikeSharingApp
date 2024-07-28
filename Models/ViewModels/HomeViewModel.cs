using System.ComponentModel.DataAnnotations;
namespace BikeSharingApp.Models
{
    public class MultipleListsViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Bike> Bikes { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}
