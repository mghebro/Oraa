using AutoMapper;
using FluentValidation;
using ORAA.DTO;
using ORAA.Models;
using ORAA.Request;

namespace ORAA.Helpers;

public class CollectionsMappingProfile : Profile
{
    public CollectionsMappingProfile()
    {
        CreateMap<Collections, CollectionsDTO>().ReverseMap();
        CreateMap<Collections, AddCollection>().ReverseMap();
    }
}
