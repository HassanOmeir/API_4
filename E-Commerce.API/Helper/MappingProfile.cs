using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.Basket;

namespace E_Commerce.API.Helper
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<ProductBrand,BrandTypeDTO>();
            CreateMap<ProductType,BrandTypeDTO>();

            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(destination => destination.BrandName, options => options.MapFrom(source => source.ProductBrand.Name))
                .ForMember(destination => destination.TypeName, options => options.MapFrom(source => source.ProductType.Name))
                .ForMember(destination => destination.PictureUrl, options => options.MapFrom<PictureUrlResolver>());  //another way for mapping 

            CreateMap<CustomerBasket,BasketDTO>().ReverseMap();
            CreateMap<BasketItemDTO , BasketItem>().ReverseMap();
		}
    }
}
