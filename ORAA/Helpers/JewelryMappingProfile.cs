using AutoMapper;
using ORAA.DTO;
using ORAA.Models;

namespace ORAA.Helpers;

public class JewelryMappingProfile : Profile
{
    public JewelryMappingProfile()
    {
        CreateMap<Jewelery, JeweleryDTO>();
    }
}
