using AutoMapper;
using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Repository.IRepository;

namespace Task_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin_User")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
           
        }
        [HttpGet("Projects")]
        public IActionResult GetAllProjects()
        {
            var projectDtoList = _projectRepository.GetAllProjects();
            return Ok(projectDtoList);
        }
        [HttpPost]
        public  IActionResult CreateProject([FromBody]ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest();
            }
            _projectRepository.CreateProjects(projectDto);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateProjects([FromBody]ProjectDto projectDto)
        {
            _projectRepository.UpdateProjects(projectDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            if(id == 0) return BadRequest();
            var projectInDb = _projectRepository.GetProjects(id);
            _projectRepository.DeleteProjects(projectInDb);
            _projectRepository.save();
            return Ok(new { message = "Data Deleted" });
        }
    }
}
