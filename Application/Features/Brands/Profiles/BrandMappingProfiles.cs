using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Brands.Profiles;

public class BrandMappingProfiles : Profile
{
    public BrandMappingProfiles()
    {
        CreateMap<Brand, CreateBrandCommand>().ReverseMap();
        CreateMap<Brand, CreatedBrandResponse>().ReverseMap();

        CreateMap<Brand, UpdatedBrandCommand>().ReverseMap();
        CreateMap<Brand, UpdatedBrandResponse>().ReverseMap();

        CreateMap<Brand, DeletedBrandCommand>().ReverseMap();
        CreateMap<Brand, DeletedBrandResponse>().ReverseMap();

        CreateMap<Brand, GetListBrandListItemDto>().ReverseMap();
        CreateMap<Brand, GetByIdBrandResponse>().ReverseMap();
        CreateMap<IPaginate<Brand>, GetListResponse<GetListBrandListItemDto>>().ReverseMap();
    }
}

