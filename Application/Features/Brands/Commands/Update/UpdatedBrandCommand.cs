using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Update;

public class UpdatedBrandCommand : IRequest<UpdatedBrandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }


    public class UpdatedBrandCommentHandler : IRequestHandler<UpdatedBrandCommand, UpdatedBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public UpdatedBrandCommentHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedBrandResponse> Handle(UpdatedBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            brand = _mapper.Map(request, brand);

            await _brandRepository.UpdateAsync(brand);

            UpdatedBrandResponse response = _mapper.Map<UpdatedBrandResponse>(brand);
            return response;
        }
    }
}

