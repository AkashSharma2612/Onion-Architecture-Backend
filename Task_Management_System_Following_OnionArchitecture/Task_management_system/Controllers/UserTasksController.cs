using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.DBContext;
using ServiceLayer.Repository.IRepository;

namespace Task_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTasksController : ControllerBase
    {
        private readonly ITaskUsersRepository _taskUsersRepository;
        public UserTasksController(ITaskUsersRepository taskUsersRepository)
        {
          _taskUsersRepository= taskUsersRepository;
        }
        [HttpGet]
        public IActionResult GetAllTaskUsers()
        {
            var taskUserList=_taskUsersRepository.GetAllTaskUser();
            return Ok(taskUserList);
        }
        [HttpPost]
        public IActionResult AddTasksUser(TaskUserDto taskUserDto)
        {
            _taskUsersRepository.AddTaskUser(taskUserDto);
            return Ok();
        }

    }
}
