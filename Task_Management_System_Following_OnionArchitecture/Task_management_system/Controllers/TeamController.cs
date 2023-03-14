using AutoMapper;
using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.DBContext;
using ServiceLayer.Repository.IRepository;
using System.Data;

namespace Task_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin_User")]
    public class TeamController : ControllerBase
    {
       private readonly ITeamRepository _teamRepository;
       
        public TeamController( ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        [HttpGet]
        public IActionResult GetAllTeams()  
        {
          var teamlist= _teamRepository.GetAllTeams();
            return Ok(teamlist);
        }
        [HttpPost]
        public IActionResult SaveTeams([FromBody]TeamDto teamDto)
        {
           _teamRepository.AddTeam(teamDto);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateTeams([FromBody] TeamDto teamDto)
        {
           _teamRepository.UpdateTeam(teamDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            if (id == 0) return BadRequest();
            var teamInDb = _teamRepository.GetTeams(id);
            _teamRepository.DeleteTeam(teamInDb);
            _teamRepository.save();
            return Ok(new { message = "Data Deleted" });
        }
    }
}
