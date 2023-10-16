using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Update;

public class UpdatedBrandCommand : IRequest<UpdatedBrandResponse>, ICacheRemoverRequest
{
    // Id özellik get ve set metotlarına sahiptir.
    public Guid Id { get; set; }

    // Name özellik get ve set metotlarına sahiptir.
    public string Name { get; set; }

    // CacheKey özelliği bir boş dize döndürür.
    public string CacheKey => "";

    // Bypasscache özelliği get metoduyla erişilebilir.
    public bool Bypasscache { get; }

    // CacheGroupKey özelliği "GetBrands" olarak başlatılır.
    public string? CacheGroupKey => "GetBrands";

    public class UpdatedBrandCommentHandler : IRequestHandler<UpdatedBrandCommand, UpdatedBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        // Gerekli bağımlılıkların enjekte edildiği yer
        public UpdatedBrandCommentHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        // Handle metodu, UpdatedBrandCommand taleplerini işler.
        public async Task<UpdatedBrandResponse> Handle(UpdatedBrandCommand request, CancellationToken cancellationToken)
        {
            // İlgili brand nesnesi, Id'ye göre GetAsync metoduyla alınır.
            Brand? brand = await _brandRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            // Brand nesnesi, gelen request verileriyle haritalanır.
            brand = _mapper.Map(request, brand);

            // Güncellenmiş brand nesnesi veritabanına kaydedilir.
            await _brandRepository.UpdateAsync(brand);

            // Güncellenmiş brand nesnesi UpdatedBrandResponse türüne haritalanır ve yanıt olarak döndürülür.
            UpdatedBrandResponse response = _mapper.Map<UpdatedBrandResponse>(brand);
            return response;
        }
    }

}

