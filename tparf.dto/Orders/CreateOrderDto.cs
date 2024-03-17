using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tparf.dto.Orders
{
    public class CreateOrderDto
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Adress { get; set; }
    }
}
