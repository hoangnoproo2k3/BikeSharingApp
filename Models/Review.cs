using System;
using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Booking Booking { get; set; }
    }
}