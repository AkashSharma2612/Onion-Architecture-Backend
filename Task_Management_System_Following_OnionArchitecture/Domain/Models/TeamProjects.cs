using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TeamProjects
    {
        [Key]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        [Key]
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
