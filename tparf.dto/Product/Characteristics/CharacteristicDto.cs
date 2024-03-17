using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tparf.dto.Product.Characteristics
{
    public class CharacteristicDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public long ProductId { get; set; }

    }
}
