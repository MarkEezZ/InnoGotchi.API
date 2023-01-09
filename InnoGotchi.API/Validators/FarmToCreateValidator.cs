using FluentValidation;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Validators
{
    public class FarmToCreateValidator : AbstractValidator<FarmToCreate>
    {
        public FarmToCreateValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("You did not enter a name.").MaximumLength(50)
                .WithMessage("Name should not be longer than 50 characters.")
                .Must(l => !l.Contains(" ")).WithMessage("Login must not contain spaces");
        }
    }
}
