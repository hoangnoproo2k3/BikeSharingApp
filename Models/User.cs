using System;
using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string Phone { get; set; }

        public int RoleId { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Role Role { get; set; }
    }
}