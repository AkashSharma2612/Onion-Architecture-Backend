using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class TeamUserDto
    {
        public string TeamName { get; set; }

        public int TeamId { get; set; }
        public List<int> TeamIds { get; set; }

        public string ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; }
    }
}
