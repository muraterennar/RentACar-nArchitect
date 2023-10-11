using FluentValidation;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(b => b.Name).MinimumLength(3).MaximumLength(50).NotEmpty();
    }
}

