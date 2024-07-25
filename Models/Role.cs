using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}