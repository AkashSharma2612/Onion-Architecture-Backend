using Application.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DBContext
{
    public class ApplicationDbContext :DbContext , IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Team> Teams { get;set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<UTask> Tasks { get; set; }

        public DbSet<UserTasks> UserTasks { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        public DbSet<TeamProjects> TeamProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //many to many relationship b/w teams and projects
            modelBuilder.Entity<TeamProjects>()
                .HasKey(tp => new { tp.ProjectId, tp.TeamId });

            modelBuilder.Entity<TeamProjects>()
                .HasOne(tp => tp.Project)
                .WithMany(p => p.TeamProjects)
                .HasForeignKey(tp => tp.ProjectId);

            modelBuilder.Entity<TeamProjects>()
                .HasOne(tp => tp.Team)
                .WithMany(t => t.TeamProjects)
                .HasForeignKey(tp => tp.TeamId);
            // many to many b/w team and Applicationuser
            modelBuilder.Entity<UserTeam>()
                .HasKey(tp => new { tp.ApplicationUserId, tp.TeamId });

            modelBuilder.Entity<UserTeam>()
                .HasOne(tp => tp.ApplicationUser)
                .WithMany(p => p.UserTeams)
                .HasForeignKey(tp => tp.ApplicationUserId);

            modelBuilder.Entity<UserTeam>()
                .HasOne(tp => tp.Team)
                .WithMany(t => t.UserTeams)
                .HasForeignKey(tp => tp.TeamId);
            // many to many b/w Task and Applicationuser
            modelBuilder.Entity<UserTasks>()
                .HasKey(tp => new { tp.TaskId, tp.ApplicationUserId });

            modelBuilder.Entity<UserTasks>()
                .HasOne(tp => tp.UTask)
                .WithMany(p => p.UserTasks)
                .HasForeignKey(tp => tp.TaskId);

            modelBuilder.Entity<UserTasks>()
                .HasOne(tp => tp.ApplicationUser)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(tp => tp.ApplicationUserId);


        }

    }
}
