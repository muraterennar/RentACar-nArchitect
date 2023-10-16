using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Delete;

public class DeletedBrandCommand : IRequest<DeletedBrandResponse>, ICacheRemoverRequest
{
    public Guid Id { get; set; } // Bir özellik (property) tanımlanmış, bu özellik bir GUID (Global Unique Identifier) türünde bir ID'yi temsil ediyor.

    public string CacheKey => ""; // Bir özellik tanımlanmış, ancak her zaman boş bir dize döndüren bir get erişimci (accessor) kullanılmış.

    public bool Bypasscache { get; } // Bir özellik tanımlanmış, bu özellik salt okunur (read-only) ve bir önbelleği atlayıp atlamama durumunu temsil ediyor.

    public string? CacheGroupKey => "GetBrands"; // Bir özellik tanımlanmış, ve bu özellik "GetBrands" dizesini döndüren bir get erişimci kullanmış. "?" işareti, bu özelliğin null olabileceğini belirtiyor.

    public class DeletedBrandCommentHandler : IRequestHandler<DeletedBrandCommand, DeletedBrandResponse>
    {
        private readonly IBrandRepository _brandRepository; // IBrandRepository türünden bir nesne enjekte edilen bir alan (field) tanımlanmış.
        private readonly IMapper _mapper; // IMapper türünden bir nesne enjekte edilen bir alan tanımlanmış.

        // Gerekli bağımlılıkların enjekte edildiği yer
        public DeletedBrandCommentHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<DeletedBrandResponse> Handle(DeletedBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken); // _brandRepository aracılığıyla bir Brand nesnesi getiriliyor.

            brand = _mapper.Map(request, brand); // _mapper aracılığıyla bir nesne haritalanıyor.

            await _brandRepository.DeleteAsync(brand); // _brandRepository aracılığıyla bir nesne siliniyor.

            DeletedBrandResponse response = _mapper.Map<DeletedBrandResponse>(brand); // _mapper aracılığıyla bir DeletedBrandResponse nesnesi oluşturuluyor ve döndürülüyor.

            return response;
        }
    }

}

