
using Domain.Dto;
using Domain.Models;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.DBContext;
using ServiceLayer.Repository.IRepository;

namespace Task_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService,ApplicationDbContext context)
        {
            _userService = userService;     
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser() 
        {
           var userList=await _userService.GetAll();
            return Ok(userList);
        }
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginVm)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.AuthenticateUser(loginVm);
            if (user == null)
                return BadRequest(new { message = "wrong user password and UserName" });
            return Ok(user);
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] ApplicationUserDto model)
        {
            // here we will check the model is valid or not 
            if (model == null || !ModelState.IsValid) return BadRequest();

            // here we will create a new instance of ApplicationUser and populate its properties with the data from the DTO.
            var ApplicationUserDetail = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.Password,
                Role=model.Role,
            };

            // here we will check the user is already register or not.
            if (!await _userService.IsUnique(model.UserName)) return Ok(new { Status = 0, Message = "You are already register go to login" });

            // here we will register the user.
            var registerUser = await _userService.RegisterUser(ApplicationUserDetail);

            //  if user is register successfully or not then take decision accordingly.
            if (!registerUser) return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(new { Status = 1, Message = "Register successfully!!!" });
        }
    }
}
