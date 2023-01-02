using FluentValidation;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Validators
{
    public class BodyPartDtoValidator : AbstractValidator<BodyPartDto>
    {
        public BodyPartDtoValidator()
        {
            RuleFor(b => b.Name).NotNull().NotEmpty().WithMessage("Name field couldn't be empty");
            RuleFor(b => b.FileName).NotNull().NotEmpty().Must(o =>
            {
                if (o != null)
                    return (o.Contains(".png") || o.Contains(".jpg"));
                else
                    return false;
            }).WithMessage("FileName field couldn't be empty and must contain file extention");
        }
    }
}
