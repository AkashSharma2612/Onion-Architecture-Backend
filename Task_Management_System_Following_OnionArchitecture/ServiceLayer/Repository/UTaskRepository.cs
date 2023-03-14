using AutoMapper;
using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.DBContext;
using ServiceLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository 
{
    public class UTaskRepository : IUTaskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UTaskRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddTask(TaskDto task)
        {
            if (task == null)
            {
                return false;
            }
            else
            {
                var taskDto = _mapper.Map<UTask>(task);
                _context.Add(taskDto);
                return save();
            }
        }

        public bool DeleteTask(UTask task)
        {
            try
            {
                var taskInDb = _context.Tasks.FirstOrDefault(e => e.Id == task.Id);
                if (taskInDb == null)
                {
                    return false;
                }
                else
                {
                    _context.Tasks.Remove(taskInDb);
                    return save();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<TaskDto> GetAllTasks()
        {
            return (from task in _context.Tasks
                    join project in _context.Projects
                    on task.ProjectId equals project.Id
                    select new TaskDto
                    {
                        ProjectId = task.ProjectId,
                        Id = task.Id,
                        Title = task.Title,
                        DueDate= task.DueDate,
                        IsComplete= task.IsComplete, 
                        
                    }).ToList();
        }

        public UTask GetTask(int taskId)
        {
            return _context.Tasks.FirstOrDefault(x => x.Id == taskId);
        }

        public bool save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool UpdateTask(TaskDto task)
        {
            if (task == null)
            {
                return false;
            }
            else
            {
                var taskDto = _mapper.Map< UTask>(task);
                _context.Update(taskDto);
                return save();
            }

        }

    }
}
