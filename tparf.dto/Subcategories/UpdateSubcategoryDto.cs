﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tparf.dto.Subcategories
{
    public class UpdateSubcategoryDto
    {
        public string Name { get; set; }
        public string IconCss { get; set; }
        public string ImageUrl { get; set; }
        public long CategoryId { get; set; }
    }
}
