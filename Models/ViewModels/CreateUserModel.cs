namespace BikeSharingApp.Models
{
    public class CreateUserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }
        public string Password { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
