using System.ComponentModel.DataAnnotations.Schema;

namespace tparf.api.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public Cart Cart { get; set; }
        [ForeignKey("CartId")]
        public long CartId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Adress { get; set; }
        [ForeignKey("StatusId")]
        public OrderStatus Status { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
