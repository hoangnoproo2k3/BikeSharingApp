using System.ComponentModel.DataAnnotations;

namespace BikeSharingApp.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., "Pending", "Approved", "Rejected"
    }
}
