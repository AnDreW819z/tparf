using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tparf.dto.Product.Descriptions
{
    public class DescriptionDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public long ProductId { get; set; }
    }
}
