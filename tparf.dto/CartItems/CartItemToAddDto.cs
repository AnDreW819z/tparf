﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tparf.dto.CartItems
{
    public class CartItemToAddDto
    {
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public int Qty { get; set; }
    }
}