using AutoMapper;
using Domain.Dto;
using Domain.Models;
using Persistence.DBContext;
using ServiceLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TeamRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddTeam(TeamDto teamDto)
        {
            if (teamDto == null) 
            { 
                return false;
            }
           var team = _mapper.Map<Team>(teamDto);
          _context.Add(team);
            return save();
        }
        public bool DeleteTeam(Team team)
        {
           
            _context.Remove(team);
            return save();
        }

        public ICollection<Team> GetAllTeams()
        {
            return _context.Teams.ToList();
        }

        public Team GetTeams(int teamId)
        {
            return _context.Teams.FirstOrDefault(x => x.Id == teamId);
        }

        public bool save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool UpdateTeam(TeamDto teamDto)
        {
            if (teamDto == null)
            {
                return false;
            }
            var team = _mapper.Map< Team>(teamDto);
            _context.Update(team);
            return save();
        }
    }
}
