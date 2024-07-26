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
    }
}