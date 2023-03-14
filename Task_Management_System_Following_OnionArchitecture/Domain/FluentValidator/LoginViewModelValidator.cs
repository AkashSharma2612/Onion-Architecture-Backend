using Domain.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FluentValidator
{
    public class LoginViewModelValidator: AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {

             RuleFor(x => x.UserName).Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!")
            .Length(2, 25);
             RuleFor(x => x.Password).NotEmpty().Length(6,12);


        }
    }
}
