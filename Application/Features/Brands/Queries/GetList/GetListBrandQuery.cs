using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requets;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery : IRequest<GetListResponse<GetListBrandListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Sayfa isteğini temsil eden bir nesneye erişim sağlar.

    public string CacheKey => $"GetListBrandQuery({PageRequest.PageIndex}, {PageRequest.PageSize})"; // Önbellek anahtarını oluşturur, sayfa isteğinin sayfa numarası ve boyutuyla birlikte.

    public bool Bypass { get; } // Önbelleği atlayıp atlamamak için bir bayrak değeri.

    public TimeSpan? SlidingExpiration { get; } // Kaydın ne kadar süreyle önbellekte tutulacağını belirler.

    public string? CacheGroupKey => "GetBrands"; // Önbellek grup anahtarıdır ve "GetBrands" olarak sabittir.

    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandListItemDto>>
    {
        private readonly IBrandRepository _brandRepository; // Marka verilerine erişim sağlayan bir veri deposu.
        private readonly IMapper _mapper; // Veri nesneleri arasında dönüşüm yapmayı sağlayan bir nesne Mapper.

        public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository; // Veri deposu ve nesne haritası enjekte edilir.
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBrandListItemDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            // Sayfalı bir şekilde marka verilerini almak için veri deposuna erişilir.
            IPaginate<Brand>? brands = await _brandRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            // Alınan veriler, GetListBrandListItemDto türündeki verilere dönüştürülür.
            GetListResponse<GetListBrandListItemDto> response = _mapper.Map<GetListResponse<GetListBrandListItemDto>>(brands);

            return response; // Dönüştürülen veri yanıt olarak döndürülür.
        }
    }

}

