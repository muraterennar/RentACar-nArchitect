using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetById;

public class GetByIdBrandQuery : IRequest<GetByIdBrandResponse>
{
    public Guid Id { get; set; }

    public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, GetByIdBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public GetByIdBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<GetByIdBrandResponse> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            // Belirtilen 'Id' değeriyle bir marka kaydını getiriyoruz.
            Brand? brand = await _brandRepository.GetAsync(
                predicate: b => b.Id == request.Id, // Marka 'Id' koşuluyla
                cancellationToken: cancellationToken,    // İptal belirteci
                withDeleted: true                    // Silinmiş markaları da getir (opsiyonel)
            );

            // Marka nesnesini GetByIdBrandResponse türüne dönüştürüyoruz.
            GetByIdBrandResponse response = _mapper.Map<GetByIdBrandResponse>(brand);

            // Dönüştürülmüş yanıtı geri döndürüyoruz.
            return response;
        }

    }
}

