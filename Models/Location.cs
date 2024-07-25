using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Coordinates { get; set; }
    }
}