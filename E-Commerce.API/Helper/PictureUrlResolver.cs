using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities;

namespace E_Commerce.API.Helper
{
	public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
	{
		private readonly IConfiguration _configuration;

		public PictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;          // used to get the base url that we added in the appsettings
		}
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)    // of the interface to resolve
		{
			return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
		}
	}
}
