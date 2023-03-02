using FluentValidation;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Validators
{
    public class FarmToCreateValidator : AbstractValidator<FarmToCreate>
    {
        public FarmToCreateValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("Farm name field is empty.").MaximumLength(50)
                .WithMessage("Farm name should not be longer than 50 characters.")
                .Must(l => !l.Contains(" ")).WithMessage("Farm name must not contain spaces");
        }
    }
}
