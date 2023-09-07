using Application.Features.Brands.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Brands.Profiles;

public class BrandMappingProfiles : Profile
{
    public BrandMappingProfiles()
    {
        CreateMap<Brand, CreateBrandCommand>().ReverseMap();
        CreateMap<Brand, CreatedBrandResponse>().ReverseMap();
    }
}

