using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications
{
    public class ProductSpecificationParameters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingParametersEnum? Sort { get; set; }




        private const int MaxPageSize = 10;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 3;

        public int pageSize
		{
            get { return _pageSize; }
            set { _pageSize = value>MaxPageSize || value<=0 ? MaxPageSize : value; }
        }

        private string? _search;

        public string? Search
        {
            get => _search; 
            set => _search = value?.Trim().ToLower(); 
        }

    }
}
