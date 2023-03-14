using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.DBContext;
using ServiceLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository
{
    public class TaskUsersRepository:ITaskUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskUsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskUserDto> GetAllTaskUser()
        {
            var userTasklist = (from tasks in _context.Tasks
                            join taskusers in _context.UserTasks
                            on tasks.Id equals taskusers.TaskId
                            select new TaskUserDto
                            {
                                ProjectId = _context.Tasks.Where(task => task.Id == tasks.ProjectId).FirstOrDefault().ProjectId,
                                ProjectName= _context.Tasks.Where(task => task.Id == tasks.ProjectId).FirstOrDefault().Project.Name,
                                TaskId = taskusers.TaskId,
                                Title =_context.Tasks.Where(task=>task.Id==taskusers.TaskId).FirstOrDefault().Title,
                                DueDate=_context.Tasks.Where(task=>task.Id==taskusers.TaskId).FirstOrDefault().DueDate,
                                IsCompleted=_context.Tasks.Where(task=>task.Id==taskusers.TaskId).FirstOrDefault().IsComplete,
                                ApplicationUserName = _context.Users.Where(user => user.Id == taskusers.ApplicationUserId).FirstOrDefault().UserName
                                ,
                                ApplicationUserId = taskusers.ApplicationUserId,
                                
                            }).ToList();
            return userTasklist;
            
        }
        [HttpPost]
        public bool AddTaskUser(TaskUserDto taskUserDto)
        {
            List<UserTasks> taskUsers = new List<UserTasks>();
            foreach (var taskUserId in taskUserDto.TaskIds)
            {
                UserTasks userTasks = new UserTasks()
                {
                    ApplicationUserId = taskUserDto.ApplicationUserId,
                     TaskId = taskUserId
                };
                 taskUsers.Add(userTasks);
            }
            _context.UserTasks.AddRange(taskUsers);
            _context.SaveChanges(); 
            return true;
        }
        
    }
}
