using Domain.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FluentValidator
{
    public class ApplicatioUserDtoValidator: AbstractValidator<ApplicationUserDto>
    {
        public ApplicatioUserDtoValidator()
        {

            RuleFor(x => x.UserName).Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!")
            .Length(2, 25);
            RuleFor(x => x.Password).NotEmpty().WithErrorCode("password").Length(5,12);
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithName("please confirm password");
            RuleFor(x => x.Email).NotEmpty().WithName("first enter email").WithErrorCode("400");
            RuleFor(x => x.Role).NotEmpty().WithName("Enter role first").WithErrorCode("400");
        }
    }
}
