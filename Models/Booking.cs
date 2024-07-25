using System;
using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int BikeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Note { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public User Customer { get; set; }
        public Bike Bike { get; set; }
    }
}