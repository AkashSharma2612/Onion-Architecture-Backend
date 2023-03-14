using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserTeam
    {
        [Key]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        [Key]
        public string ApplicationUserId { get; set; }
        public ApplicationUser  ApplicationUser { get; set; }
    }
}
