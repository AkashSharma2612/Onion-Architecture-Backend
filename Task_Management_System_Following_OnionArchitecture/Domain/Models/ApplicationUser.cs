
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Role { get; set; }
        public ICollection<UserTasks>UserTasks { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; }
    }
}
