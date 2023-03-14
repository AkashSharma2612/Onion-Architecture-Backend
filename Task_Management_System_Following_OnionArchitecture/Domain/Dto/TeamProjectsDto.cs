using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class TeamProjectsDto
    {
        public int TeamId { get; set; }
        public List<int> TeamIds { get; set; }
        public string TeamName { get; set; }

        public int ProjectId { get; set; }
        

        public string ProjectName { get; set; }

    }
}
