using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse>, ITransactionRequest, ICacheRemoverRequest, ILoggableRequest
{
    // "Name" adında bir public property oluşturuluyor.
    public string Name { get; set; }

    // "CacheKey" adında bir public read-only property oluşturuluyor ve değeri boş bir dize olarak ayarlanıyor.
    public string CacheKey => "";

    // "Bypasscache" adında bir public read-only property oluşturuluyor.
    public bool Bypasscache { get; }

    // "CacheGroupKey" adında bir public read-only nullable property oluşturuluyor ve değeri "GetBrands" olarak ayarlanıyor.
    public string? CacheGroupKey => "GetBrands";

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>, ITransactionRequest
    {
        // İşlem yapılacak sınıflara ait gerekli bağımlılıklar enjekte ediliyor.
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        // Oluşturucu metot, sınıfın bağımlılıklarını enjekte ediyor.
        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        // "Handle" metodu, "CreateBrandCommand" türündeki isteği işliyor ve "CreatedBrandResponse" türünden bir yanıt döndürüyor.
        public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            // Marka adının benzersiz olup olmadığını kontrol eden iş kuralları uygulanıyor.
            await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

            // Yeni bir "Brand" nesnesi oluşturuluyor ve istekten haritalanıyor.
            Brand brand = _mapper.Map<Brand>(request);
            brand.Id = Guid.NewGuid();

            // Marka veritabanına ekleniyor.
            await _brandRepository.AddAsync(brand);

            // Eklenen markanın bilgileri "CreatedBrandResponse" türüne dönüştürülüyor ve yanıt olarak gönderiliyor.
            CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(brand);
            return createdBrandResponse;
        }
    }
}