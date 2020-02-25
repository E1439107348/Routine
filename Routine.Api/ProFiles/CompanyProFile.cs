using AutoMapper;
using Routine.Api.Entities;
using Routine.Api.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.ProFiles
{
    /// <summary>
    /// 对象映射器 
    /// </summary>
    public class CompanyProFile : Profile
    {
        public CompanyProFile()
        {
            //从Company映射到CompanyDto
            CreateMap<Company, CompanyDto>()
                .ForMember(
                dest=>dest.CompanyName,//目标
                opt=>opt.MapFrom(src=>src.Name)
                );

            //从CompanyDto映射到Company
            CreateMap<CompanyDto, Company>()
              .ForMember(
              dest => dest.Name,//目标
              opt => opt.MapFrom(src => src.CompanyName)
              );

            //从CompanyAddDto映射到Company
            CreateMap<CompanyAddDto, Company>();

        }
    }
}
