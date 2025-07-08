using AutoMapper;
using ORAA.Models;
using ORAA.Request;

namespace ORAA.Helpers;
public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<AddCart, Cart>();
        CreateMap<AddCartItem, CartItem>();
    }
}
