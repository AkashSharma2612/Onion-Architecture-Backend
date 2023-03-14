using AutoMapper;
using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectDto,Project>().ReverseMap();
            CreateMap<TaskDto, UTask>();
            CreateMap<TeamDto, Team>().ReverseMap();
            CreateMap<ApplicationUserDto, ApplicationUser>().ReverseMap();
            CreateMap<TeamProjectsDto,TeamProjects>().ReverseMap();
        }
    }
}
