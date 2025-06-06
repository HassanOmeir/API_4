﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.Basket
{
	public class CustomerBasket
	{
        public string Id { get; set; }
        public int? DeleveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
