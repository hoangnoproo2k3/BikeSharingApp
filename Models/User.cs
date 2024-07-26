using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeSharingApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

        public Role Role { get; set; }
        public ICollection<Bike> OwnedBikes { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

}