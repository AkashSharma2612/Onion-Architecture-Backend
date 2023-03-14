using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<TeamProjects> TeamProjects { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; }
    }
}
