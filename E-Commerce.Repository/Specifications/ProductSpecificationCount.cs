using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Specifications
{
	public class ProductSpecificationCount : BaseSpecifications<Product>
	{
		public ProductSpecificationCount(ProductSpecificationParameters specs)
			: base(
				 product =>
					  (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value)
					  &&
					  (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value)
				      &&
					  (string.IsNullOrWhiteSpace(specs.Search) || product.Name.ToLower().Contains(specs.Search))

				  )
		{

		}
	}
}
