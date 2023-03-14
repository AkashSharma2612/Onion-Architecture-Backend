using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Repository.IRepository;

namespace Task_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskInformationController : ControllerBase
    {
        private readonly ITaskInformationDtoRepository _taskInformationDtoRepository;
        public TaskInformationController(ITaskInformationDtoRepository taskInformationDtoRepository)
        {
            _taskInformationDtoRepository = taskInformationDtoRepository;
        }
        [HttpGet]
        public IActionResult GetTAskInformations() 
        {
            var taskinformationList = _taskInformationDtoRepository.GetAll();
            return Ok(taskinformationList);
        }
    }
}
