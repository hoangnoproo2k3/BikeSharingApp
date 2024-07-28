using System;
using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? BikeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int StatusId { get; set; }
        public User Customer { get; set; }
        public Bike Bike { get; set; }
        public Status Status { get; set; }
        // public ICollection<Review> Reviews { get; set; }
    }
}