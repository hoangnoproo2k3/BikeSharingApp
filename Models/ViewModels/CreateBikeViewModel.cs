using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeSharingApp.Models
{
    public class CreateBikeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bike name is required.")]
        [Display(Name = "Bike Name")]
        [StringLength(100, ErrorMessage = "Bike name cannot be longer than 100 characters.")]
        public string BikeName { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public IFormFile? Img { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Required(ErrorMessage = "Price per hour is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price per hour must be greater than 0.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Price Per Hour")]
        public decimal PricePerHour { get; set; }

        public List<SelectListItem> Locations { get; set; }

        [Display(Name = "Existing Image")]
        public string ExistingImg { get; set; }
    }

}
