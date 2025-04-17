using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Services
{
    public interface IProductService
	{
		Task<PaginatedResult<ProductToReturnDTO>> GetAllProductsDTOAsync(ProductSpecificationParameters SpecParameters);
		Task<ProductToReturnDTO> GetProductDTOAsync(int id);
		Task<IEnumerable<BrandTypeDTO>> GetAllBrandsDTOAsync();
		Task<IEnumerable<BrandTypeDTO>> GetAllTypesDTOAsync();
	}
}
