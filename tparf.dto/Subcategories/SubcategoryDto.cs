using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tparf.dto.Subcategories
{
    public class SubcategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IconCss { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }
    }
}
