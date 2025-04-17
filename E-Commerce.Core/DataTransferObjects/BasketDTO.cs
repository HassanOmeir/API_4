using E_Commerce.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObjects
{
	public class BasketDTO
	{
		public string Id { get; set; }
		public int? DeleveryMethodId { get; set; }
		public decimal ShippingPrice { get; set; }
		public List<BasketItemDTO> BasketItems { get; set; } = new List<BasketItemDTO>();
	}
}
