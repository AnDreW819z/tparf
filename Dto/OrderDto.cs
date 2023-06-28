using System.ComponentModel.DataAnnotations;
using tparf.Models;

namespace tparf.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrdersDate { get; set; }
        public virtual Product Product { get; set; }
    }
}
