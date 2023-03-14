using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.IRepository
{
    public interface IProjectRepository
    {
        ICollection<Project> GetAllProjects();

        bool CreateProjects(ProjectDto project);
        bool UpdateProjects(ProjectDto project);
        bool DeleteProjects(Project project);
        Project GetProjects(int projectId);
        bool save();
    }
}
