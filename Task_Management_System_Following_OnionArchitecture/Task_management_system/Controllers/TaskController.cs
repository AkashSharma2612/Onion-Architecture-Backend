using AutoMapper;
using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Repository.IRepository;
using System.Data;
using System.Runtime.Serialization.Json;

namespace Task_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin_User")]
    public class TaskController : ControllerBase
    {
        private readonly IUTaskRepository _taskRepository;
        public TaskController(IUTaskRepository taskRepository,IMapper mapper)
        {
            _taskRepository = taskRepository;
        }
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var taskList= _taskRepository.GetAllTasks();
            return Ok(taskList);
        }
        [HttpPost]
        public IActionResult CreateTask([FromBody]TaskDto task)
        {
         _taskRepository.AddTask(task);
            return Ok(task);
        }
        [HttpPut]
        public IActionResult UpdateTask([FromBody]TaskDto task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            else
            {
               _taskRepository.UpdateTask(task);
                return Ok();
            }
        }
        [HttpDelete]
        public IActionResult DeleteTask(UTask task)
        {
            _taskRepository.DeleteTask(task);
            return Ok();
        }
    }
}
