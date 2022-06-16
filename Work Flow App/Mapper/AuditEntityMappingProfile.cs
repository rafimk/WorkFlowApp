using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using Work_Flow_App.Dtos;

namespace Work_Flow_App.Mapper
{
    public class AuditEntityMappingProfile : Profile
    {
        public AuditEntityMappingProfile()
        {
            CreateMap<AuditEntity, AuditEntityDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedOn, o => o.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.CreatedBy, o => o.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.ModifiedOn, o => o.MapFrom(src => src.ModifiedOn))
                .ForMember(dest => dest.ModifiedBy, o => o.MapFrom(src => src.ModifiedBy))
                .ForMember(dest => dest.DeletedOn, o => o.MapFrom(src => src.DeletedOn))
                .ForMember(dest => dest.DeletedBy, o => o.MapFrom(src => src.DeletedBy))
                .ForMember(dest => dest.IsDeleted, o => o.MapFrom(src => src.IsDeleted));

            CreateMap<AuditEntityDto, AuditEntity>()
                .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedOn, o => o.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.CreatedBy, o => o.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.ModifiedOn, o => o.MapFrom(src => src.ModifiedOn))
                .ForMember(dest => dest.ModifiedBy, o => o.MapFrom(src => src.ModifiedBy))
                .ForMember(dest => dest.DeletedOn, o => o.MapFrom(src => src.DeletedOn))
                .ForMember(dest => dest.DeletedBy, o => o.MapFrom(src => src.DeletedBy))
                .ForMember(dest => dest.IsDeleted, o => o.MapFrom(src => src.IsDeleted));
        }
    }
}
