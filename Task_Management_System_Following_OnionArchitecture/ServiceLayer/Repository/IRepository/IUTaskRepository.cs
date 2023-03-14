using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.IRepository
{
    public interface IUTaskRepository
    {
        List<TaskDto> GetAllTasks();

        bool AddTask(TaskDto task);
        bool UpdateTask(TaskDto task);
        bool DeleteTask(UTask task);
        UTask GetTask(int taskId);
        bool save();
    }
}
