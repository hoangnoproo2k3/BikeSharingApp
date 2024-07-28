namespace BikeSharingApp.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Bike> Bikes { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public string LocationName { get; set; }
    }
}
