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

namespace ServiceLayer.Repository
{
    public class TeamProjectRepository : ITeamProjectsRepository
    {
        private readonly ApplicationDbContext _context;
        public TeamProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddTeamsProject(TeamProjectsDto teamProject)
        {  
            List< TeamProjects > teams = new List< TeamProjects >();
            foreach (var item in teamProject.TeamIds)
            {
                TeamProjects teamProjectsDto = new TeamProjects
                {
                    TeamId = item,
                    ProjectId= teamProject.ProjectId
                };
                teams.Add(teamProjectsDto);
            }
                      _context.AddRange(teams);
                       _context.SaveChanges();
              return true;
        }

        public IEnumerable<TeamProjectsDto> GetAllTeamProjects()
        {
            var teamProjectList = (from projects in _context.Projects
                                   join teamprojects in _context.TeamProjects
                                   on projects.Id equals teamprojects.ProjectId
                                   select new TeamProjectsDto
                                   {
                                       ProjectId = projects.Id,
                                       ProjectName = _context.Projects.Where(name => name.Id == teamprojects.ProjectId).FirstOrDefault().Name,
                                       TeamId = teamprojects.TeamId,
                                       TeamName = _context.Teams.Where(name => name.Id == teamprojects.TeamId).FirstOrDefault().Name
                                   }).ToList();
            return teamProjectList;
        }
    }
}
