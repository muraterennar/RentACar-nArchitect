using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules : BaseBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task BrandNameCannotBeDuplicatedWhenInserted(string name)
    {
        // Verilen 'name' değerine sahip bir marka alınmaya çalışıyor.

        Brand? result = await _brandRepository.GetAsync(b => b.Name.ToLower() == name);

        // Eğer böyle bir marka bulunursa (result null değilse), hata fırlatılır.

        if (result != null)
        {
            throw new BusinessException(BrandsMessages.BrandNameExists);
        }
    }

}

