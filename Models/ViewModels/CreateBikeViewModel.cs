using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeSharingApp.Models
{
    public class CreateBikeViewModel
    {
        public int Id { get; set; }  // Thêm thuộc tính Id

        [Required]
        public string BikeName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }

        [Required]
        public string OwnerPhone { get; set; }

        [Required]
        public string BikeStatus { get; set; }

        public string Description { get; set; }

        public IFormFile Img { get; set; }

        [Required]
        public int? LocationId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price per hour")]
        public decimal PricePerHour { get; set; }

        public List<SelectListItem> Locations { get; set; }

        public string ExistingImg { get; set; }  // Thêm thuộc tính ExistingImg
    }
}
