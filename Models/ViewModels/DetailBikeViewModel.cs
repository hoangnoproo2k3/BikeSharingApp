using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeSharingApp.Models
{
    public class DetailBikeViewModel
    {
        public Bike Bike { get; set; }
        public bool Check { get; set; }
        public string BikeName { get; set; }
    }

}
