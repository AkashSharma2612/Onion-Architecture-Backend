using Domain.Models;
using Domain.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.DBContext;
using ServiceLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Utility;

namespace ServiceLayer.Repository
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManagaer;
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;

        public UserService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManagaer = roleManager;
            _context = context;
        }

        public async Task<ApplicationUser?> AuthenticateUser(LoginViewModel loginViewModel)
        {

            var result = await _signInManager.PasswordSignInAsync
         (loginViewModel.UserName, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _userManager.
                  FindByNameAsync(loginViewModel.UserName);
                applicationUser.PasswordHash = "";
                //JWT Token
                if (await _userManager.IsInRoleAsync(applicationUser, SD.Role_Admin))
                    applicationUser.Role = SD.Role_Admin;
                //
                if (await _userManager.IsInRoleAsync(applicationUser, SD.Role_Employee))
                    applicationUser.Role = SD.Role_Employee;
                var tokenhandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, applicationUser.Id),
                    new Claim(ClaimTypes.Email, applicationUser.Email),
                    new Claim(ClaimTypes.Role,applicationUser.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenhandler.CreateToken(tokenDescriptor);
                applicationUser.Token = tokenhandler.WriteToken(token);
                applicationUser.PasswordHash = "";
                return applicationUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<IdentityUser>> GetAll()
        {

            return await _context.Users.ToListAsync();
        }

        public async Task<bool> IsUnique(string userName)
        {
            var userExist = await _userManager.FindByNameAsync(userName);
            if (userExist == null) return true;
            return false;
        }

        public async Task<bool> RegisterUser(ApplicationUser userCredentials)
        {

            // create the user here 
            // first check the role he gave is exist in the database or not
            if (await _roleManagaer.FindByNameAsync(userCredentials.Role) == null) return false;

            /*if (userCredentials.Role == SD.Role_Admin)
            {
                var CheckAdmin = await _userManager.GetUsersInRoleAsync(SD.Role_Admin);
                if (CheckAdmin.Count == 1) return false;
            }*/

            var user = await _userManager.CreateAsync(userCredentials, userCredentials.PasswordHash);
            if (!user.Succeeded) return false;
            // here assign the role to the user
            await _userManager.AddToRoleAsync(userCredentials, userCredentials.Role);
            return true;
        }

    }
}

