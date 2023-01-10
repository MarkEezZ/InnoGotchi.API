using FluentValidation;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Validators
{
    public class UserInfoDtoValidator : AbstractValidator<UserInfoDto>
    {
        public UserInfoDtoValidator()
        {
            RuleFor(u => u.Password).MinimumLength(6).MaximumLength(50).WithMessage("Password must be longer than 5 and shorter than 50 characters.");
            RuleFor(u => u.NewPassword).MinimumLength(6).MaximumLength(50).WithMessage("New password must be longer than 5 and shorter than 50 characters.");
            RuleFor(u => u.Age).Must(a => a > 0 && a <= 150).WithMessage("Login must be greater than 0 and less than 150 characters.");
        }
    }
}
