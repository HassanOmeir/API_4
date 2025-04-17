using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
	{
		// This ctor is for retrieving ONLY ONE PRODUCT based on the id .....
		public ProductSpecifications(int id) : base(product => product.Id == id)
		{
			// The includes that must be done in any ctor !!
			IncludeExpressions.Add(product => product.ProductBrand);
			IncludeExpressions.Add(product => product.ProductType);
		}


		// This ctor is for retrieving PRODUCTS BASED ON SOME CRITERIA (in our case the criteria can be brand , type) .....
		public ProductSpecifications(ProductSpecificationParameters specs) 
			: base(
				 product => 
					  (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value)
					  &&
					  (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value)
					  &&
					  (string.IsNullOrWhiteSpace(specs.Search) || product.Name.ToLower().Contains(specs.Search))

				  )
		{
			// To avoid having many parameters in the ctor , we put them all in a class "ProductSpecificationParameters"
			// and use an object from it now


			// The includes that must be done in any ctor !!
			IncludeExpressions.Add(product => product.ProductBrand);
			IncludeExpressions.Add(product => product.ProductType);

			if (specs.Sort is not null)
			{
				switch (specs.Sort)
				{
					case ProductSortingParametersEnum.NameAsc:
						OrderByAscExp = x => x.Name;
						break;
					case ProductSortingParametersEnum.NameDesc:
						OrderByDescExp = x => x.Name;
						break;
					case ProductSortingParametersEnum.PriceAsc:
						OrderByAscExp = x => x.Price;
						break;
					case ProductSortingParametersEnum.PriceDesc:
						OrderByDescExp = x => x.Price;
						break;
					default:
						OrderByAscExp = x => x.Name;
						break;
				}
			}
			else OrderByAscExp = x => x.Name;


			ApplyPagination(specs.pageSize, specs.PageIndex);
 
		}
	}
}
