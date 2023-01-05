using FluentValidation;
using InnoGotchi.API.Entities.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.API.Validators
{
    public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationDtoValidator()
        {
            RuleFor(u => u.Login).NotNull().MinimumLength(5).MaximumLength(50).WithMessage("Login must be longer than 4 and shorter than 50 characters.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Invalid email adress.").NotNull()
                .MinimumLength(5).MaximumLength(100).WithMessage("Email must be longer than 4 and shorter than 100 characters.");
            RuleFor(u => u.Password).NotNull().MinimumLength(6).MaximumLength(50).WithMessage("Password must be longer than 5 and shorter than 50 characters.");
            RuleFor(u => u.PasswordConfirm).NotNull().MinimumLength(6).MaximumLength(50).WithMessage("Password confirmation must be longer than 5 and shorter than 50 characters.");
            RuleFor(u => u.Name).MinimumLength(2).MaximumLength(50).WithMessage("Name must be longer than 1 and shorter than 50 characters.");
            RuleFor(u => u.Surname).MinimumLength(2).MaximumLength(50).WithMessage("Login must be longer than 1 and shorter than 50 characters.");
            RuleFor(u => u.Age).GreaterThan(0).LessThan(150).WithMessage("Login must be greater than 0 and less than 150 characters.");
        }
    }
}

