using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.IRepository
{
    public interface ITeamUsersRepository
    {
       IEnumerable<TeamUserDto> GetAllTeamsUsers();
        bool AddTeamUser(TeamUserDto teamUser);
    }
}
