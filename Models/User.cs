namespace tparf.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public decimal TotalOrdersPrice { get; set; }
        public Role UserRole { get; set; } 
        public ICollection<Order> Orders { get; set; }
    }
    public enum Role
    {
        Admin,
        ManufacturerManager,
        User
    }
}
