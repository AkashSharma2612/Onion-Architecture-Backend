using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Repository.IRepository;

namespace Task_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamProjectsController : ControllerBase
    {
        private readonly ITeamProjectsRepository _repository;
        public TeamProjectsController(ITeamProjectsRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAllTeamProjects() 
        { 
            var teamProjectList= _repository.GetAllTeamProjects();
            return Ok(teamProjectList);
        }
        [HttpPost]
        public IActionResult AddTeamsProject([FromBody]TeamProjectsDto teamProjectsDto) 
        { 
            _repository.AddTeamsProject(teamProjectsDto);
            return Ok();
        }
    }
}
