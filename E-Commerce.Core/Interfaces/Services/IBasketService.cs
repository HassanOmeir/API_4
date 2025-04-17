using E_Commerce.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Services
{
	public interface IBasketService
	{
		Task<BasketDTO?> GetBasketAsync(string id);
		Task<BasketDTO?> UpdataBasketAsync(BasketDTO basket);
		Task<bool> DeleteBasketAsync(string id);
	}
}
