﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tparf.dto.Categories
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IconCss { get; set; }
        public string ImageUrl { get; set; }
        public long? ParentId { get; set; }
        public List<CategoryDto>? Children { get; set; }
    }
}
