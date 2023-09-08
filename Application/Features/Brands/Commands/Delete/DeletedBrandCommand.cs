using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Delete;

public class DeletedBrandCommand : IRequest<DeletedBrandResponse>
{
    public Guid Id { get; set; }


    public class DeletedBrandCommentHandler : IRequestHandler<DeletedBrandCommand, DeletedBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public DeletedBrandCommentHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<DeletedBrandResponse> Handle(DeletedBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            brand = _mapper.Map(request, brand);

            await _brandRepository.DeleteAsync(brand);

            DeletedBrandResponse response = _mapper.Map<DeletedBrandResponse>(brand);

            return response;
        }
    }
}

