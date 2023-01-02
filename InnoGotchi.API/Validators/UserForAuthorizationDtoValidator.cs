using FluentValidation;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Validators
{
    public class UserForAuthorizationDtoValidator : AbstractValidator<UserForAuthorizationDto>
    {
        public UserForAuthorizationDtoValidator()
        {
            RuleFor(u => u.Login).NotNull().WithMessage("Please, enter a login");
            RuleFor(u => u.Password).NotNull().WithMessage("Please, enter a password");
        }
    }
}
