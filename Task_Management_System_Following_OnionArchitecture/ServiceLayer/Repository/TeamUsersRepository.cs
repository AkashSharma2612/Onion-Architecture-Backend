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
    public class TeamUsersRepository : ITeamUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public TeamUsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddTeamUser(TeamUserDto teamUser)
        {
            List<UserTeam> teams = new List<UserTeam>();
            foreach (var item in teamUser.TeamIds)
            {
                UserTeam teamUserDto = new UserTeam
                {
                    TeamId=item,
                    ApplicationUserId=teamUser.ApplicationUserId,
                    
                };
                teams.Add(teamUserDto);
            }
            _context.UserTeams.AddRange(teams);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable< TeamUserDto> GetAllTeamsUsers()
        {

            var teamlist = (from teams in _context.Teams
                            join teamusers in _context.UserTeams
                            on teams.Id equals teamusers.TeamId
                            select new TeamUserDto
                            {
                                TeamName = teams.Name,
                                TeamId = teams.Id,
                                ApplicationUserName = _context.Users.Where(user => user.Id == teamusers.ApplicationUserId).FirstOrDefault().UserName
                                ,
                                ApplicationUserId = teamusers.ApplicationUserId
                            }).ToList() ;  
               return teamlist ;
        }
    }
}
