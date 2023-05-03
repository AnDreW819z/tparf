using System.ComponentModel.DataAnnotations;

namespace tparf.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
    public enum OrderStatus
    {
        Ordered,
        Paid,
        Canceled

    }
}
