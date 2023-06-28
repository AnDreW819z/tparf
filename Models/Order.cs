using System.ComponentModel.DataAnnotations;

namespace tparf.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrdersDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Ordered;
        //[Required]
        //public virtual ApplicationUser User { get; set; }
        [Required]
        public virtual Product Product { get; set; }
    }
    public enum OrderStatus
    {
        Ordered,
        Paid,
        Canceled

    }
}
