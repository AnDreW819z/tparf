using tparf.Models;

namespace tparf.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
