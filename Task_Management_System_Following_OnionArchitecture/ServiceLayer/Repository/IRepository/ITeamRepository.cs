using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.IRepository
{
    public interface ITeamRepository
    {
        ICollection<Team> GetAllTeams();

        bool AddTeam(TeamDto teamDto);
        bool UpdateTeam(TeamDto teamDto);
        bool DeleteTeam(Team team);
        Team GetTeams(int teamId);
        bool save();
    }
}
