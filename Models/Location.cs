using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }

        public ICollection<Bike> Bikes { get; set; }
    }
}