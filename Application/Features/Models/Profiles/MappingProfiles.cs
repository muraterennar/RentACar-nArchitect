using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, GetListModelListItemDto>()
            .ForMember(destinationMember: m => m.BrandName, memberOptions: opt => opt.MapFrom(b => b.Brand.Name))
            .ForMember(destinationMember: m => m.FuelName, memberOptions: opt => opt.MapFrom(f => f.Fuel.Name))
            .ForMember(destinationMember: m => m.TransmissionName, memberOptions: opt => opt.MapFrom(t => t.Transmission.Name))
            .ReverseMap();

        CreateMap<Model, GetListByDynamicModelListItemDto>()
            .ForMember(destinationMember: m => m.BrandName, memberOptions: opt => opt.MapFrom(b => b.Brand.Name))
            .ForMember(destinationMember: m => m.FuelName, memberOptions: opt => opt.MapFrom(f => f.Fuel.Name))
            .ForMember(destinationMember: m => m.TransmissionName, memberOptions: opt => opt.MapFrom(t => t.Transmission.Name))
            .ReverseMap();

        CreateMap<IPaginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
        CreateMap<IPaginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
    }
}

