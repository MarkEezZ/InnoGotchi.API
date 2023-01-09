using FluentValidation;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Validators
{
    public class PetDtoValidator : AbstractValidator<PetDto>
    {
        public PetDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("A name field is empty.").NotNull()
                .MaximumLength(50).WithMessage("A name is longer than 50 charactres.")
                .Must(n => !n.Contains(" ")).WithMessage("Login must not contain spaces");
            RuleFor(p => p.BodyId).NotNull().WithMessage("A body was not selected.");
            RuleFor(p => p.EyesId).NotNull().WithMessage("An eyes was not selected.");
            RuleFor(p => p.MouthId).NotNull().WithMessage("A mouth was not selected.");
        }
    }
}
