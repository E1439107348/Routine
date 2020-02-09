﻿using AutoMapper;
using Routine.Api.Entities;
using Routine.Api.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.ProFiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName}{src.LastName}"))

                  .ForMember(dest => dest.GenderDisplay,
                  opt => opt.MapFrom(src => src.Gender.ToString()))

                .ForMember(dest => dest.Age,
                opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year));
        }
    }
}