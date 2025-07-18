using FluentValidation;
using ORAA.Models;

namespace ORAA.Validations;

public class CollectionValidator : AbstractValidator<Collections>
{
    public CollectionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
        RuleFor(x => x.PhotoURL)
            .NotEmpty().WithMessage("Description is required.");
          
    }
}
