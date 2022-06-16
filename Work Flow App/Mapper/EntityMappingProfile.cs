using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using Work_Flow_App.Dtos;

namespace Work_Flow_App.Mapper
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Entity, EntityDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id));

            CreateMap<EntityDto, Entity>()
                .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id));
        }
    }
}
