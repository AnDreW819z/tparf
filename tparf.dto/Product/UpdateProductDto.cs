﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tparf.dto.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Article { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }
        public long СategoryId { get; set; }
        public long ManufacturerId { get; set; }
        public int CurrencyId {get; set;}
    }
}
