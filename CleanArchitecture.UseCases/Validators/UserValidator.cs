using CleanArchitecture.UseCases.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.UseCases.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Username).NotEmpty().MinimumLength(3).WithMessage("Username is required and must be at least 3 characters.");
            RuleFor(x => x.Roles).NotNull().WithMessage("At least one role is required.");
        }
    }
}
