using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Repository.IRepository;

namespace Task_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamUsersController : ControllerBase
    {
        private readonly ITeamUsersRepository _teamUsersRepository;
        public TeamUsersController(ITeamUsersRepository teamUsersRepository)
        {
            _teamUsersRepository = teamUsersRepository;
        }
        [HttpGet]
        public IActionResult GetAllTeamUser()
        {
         var teamUserList=_teamUsersRepository.GetAllTeamsUsers();
            return Ok(teamUserList);
        }
        [HttpPost]
        public IActionResult AddTeamUser( [FromBody]TeamUserDto teamUserDto)
        {
            _teamUsersRepository.AddTeamUser(teamUserDto);
            return Ok();
        }
    }
}
