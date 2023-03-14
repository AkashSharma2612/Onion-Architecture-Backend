using Domain.Dto;
using Domain.Models;
using Persistence.DBContext;
using ServiceLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer .Repository
{
    public class TaskInforamationDtoRepository :ITaskInformationDtoRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskInforamationDtoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskInformationDto> GetAll()
        {
            var taskDetails =( from user in _context.Users
                        join teamUser in _context.UserTeams on user.Id equals teamUser.ApplicationUserId
                        join teamProject in _context.TeamProjects on teamUser.TeamId equals teamProject.TeamId
                        join taskUser in _context.UserTasks on teamUser.ApplicationUserId equals taskUser.ApplicationUserId
                        select new TaskInformationDto
                        {
                            ProjectName=_context.Projects.Where(x=>x.Id==teamProject.ProjectId).FirstOrDefault().Name,
                            TeamName=_context.Teams.Where(x=>x.Id==teamUser.TeamId).FirstOrDefault().Name,
                            ApplicationUserId=teamUser.ApplicationUserId,
                            ApplicationUserName=_context.Users.Where(x=>x.Id== teamUser.ApplicationUserId).FirstOrDefault().UserName,
                            TaskAssign=_context.Tasks.Where(x=>x.Id==taskUser.TaskId).FirstOrDefault().Title,
                            Date= _context.Tasks.Where(x => x.Id == taskUser.TaskId).FirstOrDefault().DueDate,
                            IsCompleted= _context.Tasks.Where(x => x.Id == taskUser.TaskId).FirstOrDefault().IsComplete,
                        }).ToList();
            return taskDetails;

        }
    }
    
}
