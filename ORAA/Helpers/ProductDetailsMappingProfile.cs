using AutoMapper;
using ORAA.Models;
using ORAA.Request;
using ORAA.DTO;
namespace ORAA.Helpers
{
    public class ProductDetailsMappingProfile : Profile
    {
        public ProductDetailsMappingProfile()
        {
            CreateMap<ProductDetails, AddProductDetailsRequest>().ReverseMap();
            CreateMap<ProductDetails, AdminDTO>().ReverseMap();
            CreateMap<ProductDetails, ProductDetailsDTO>().ReverseMap();
        }
    }
}
