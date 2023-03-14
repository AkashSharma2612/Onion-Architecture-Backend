using Domain.Models;
using Domain.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.IRepository
{
    public interface IUserService
    {
        Task<List<IdentityUser>> GetAll();
        Task<bool> RegisterUser(ApplicationUser userCredentials);
        Task<bool> IsUnique(string userName);
        Task<ApplicationUser?> AuthenticateUser(LoginViewModel loginViewModel);
    }
}
