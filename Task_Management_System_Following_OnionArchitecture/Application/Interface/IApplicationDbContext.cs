using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IApplicationDbContext
    {
        DbSet<Team> Teams { get; }  
        DbSet<Project> Projects { get; }
        DbSet<UTask> Tasks { get; }
        DbSet<UserTasks> UserTasks { get; }
        DbSet <UserTeam> UserTeams { get; } 
        DbSet<TeamProjects> TeamProjects { get; }
    }
}
