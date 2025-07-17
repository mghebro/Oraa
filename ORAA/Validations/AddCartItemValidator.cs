using FluentValidation;
using ORAA.Request;
namespace ORAA.Validations;
public class AddCartItemValidator : AbstractValidator<AddCartItem>
{
    public AddCartItemValidator()
    {
        RuleFor(x => x.Size)
            .NotEmpty()
            .WithMessage("Size is required.")
            .MaximumLength(100)
            .WithMessage("Size cannot exceed 100 characters.");

        RuleFor(x => x.Engraving)
            .MaximumLength(500)
            .WithMessage("Engraving cannot exceed 500 characters.");

        RuleFor(x => x.CustomizationNotes)
            .MaximumLength(1000)
            .WithMessage("Customization notes cannot exceed 1000 characters.");
    }
}