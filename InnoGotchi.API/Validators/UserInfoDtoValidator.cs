using FluentValidation;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Validators
{
    public class UserInfoDtoValidator : AbstractValidator<UserInfoDto>
    {
        public UserInfoDtoValidator()
        {
            RuleFor(u => u.Password).NotNull().WithMessage("Password field can not be empty.").NotEmpty().MinimumLength(6).MaximumLength(50)
                .WithMessage("Password must be longer than 5 and shorter than 50 symbols");
            RuleFor(u => u.Age).Must(a => a > 0 && a <= 150).WithMessage("Login must be greater than 0 and less than 150");
        }
    }
}
