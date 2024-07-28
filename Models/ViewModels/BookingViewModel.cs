using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeSharingApp.Models
{
    public class BookingViewModel
    {
        public Bike Bike { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        [CustomValidation(typeof(BookingValidator), nameof(BookingValidator.ValidateStartDate))]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        [CustomValidation(typeof(BookingValidator), nameof(BookingValidator.ValidateEndDate))]
        public DateTime EndDate { get; set; }
    }
    public static class BookingValidator
    {
        public static ValidationResult ValidateStartDate(DateTime startDate, ValidationContext context)
        {
            if (startDate < DateTime.Now)
            {
                return new ValidationResult("Start date must be greater than or equal to the current date.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateEndDate(DateTime endDate, ValidationContext context)
        {
            var viewModel = (BookingViewModel)context.ObjectInstance;
            if (endDate < viewModel.StartDate)
            {
                return new ValidationResult("End date must be greater than or equal to the start date.");
            }
            return ValidationResult.Success;
        }
    }
}
