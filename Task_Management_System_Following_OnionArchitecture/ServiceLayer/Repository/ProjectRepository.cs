using AutoMapper;
using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProjectRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateProjects(ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return false;
            }
            var projectsDto = _mapper.Map<Project>(projectDto);
            _context.Add(projectsDto);
            return save();
        }

        public bool DeleteProjects(Project project)
        {
            if (project == null)
            {
                return false;
            }
            _context.Projects.Remove(project);
            return save();
        }

        public ICollection<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public Project GetProjects(int projectId)
        {
            return _context.Projects.FirstOrDefault(x => x.Id == projectId);
        }

        public bool save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool UpdateProjects(ProjectDto project)
        {
            if (project == null)
            {
                return false;
            }
            var projectsDto = _mapper.Map<Project>(project);
            _context.Update(projectsDto);
            return save() ;
        }
    }
}