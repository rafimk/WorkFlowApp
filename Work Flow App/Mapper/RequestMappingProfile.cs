using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using Work_Flow_App.Dtos;

namespace Work_Flow_App.Mapper
{
    public class RequestMappingProfile : Profile
    {
        public RequestMappingProfile()
        {
            CreateMap<Request, RequestDto>()
             .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
             .ForMember(dest => dest.Title, o => o.MapFrom(src => src.Title))
             .ForMember(dest => dest.Details, o => o.MapFrom(src => src.Details))
             .ForMember(dest => dest.Notes, o => o.MapFrom(src => src.Notes))
             .ForMember(dest => dest.Status, o => o.MapFrom(src => src.Status))
             .ForMember(dest => dest.Attachments, o => o.MapFrom(src => src.Attachments))
             .ForMember(dest => dest.RequestWorkFlows, o => o.MapFrom(src => src.RequestWorkFlows))
             .ForMember(dest => dest.CreatedOn, o => o.MapFrom(src => src.CreatedOn))
             .ForMember(dest => dest.CreatedBy, o => o.MapFrom(src => src.CreatedBy))
             .ForMember(dest => dest.ModifiedOn, o => o.MapFrom(src => src.ModifiedOn))
             .ForMember(dest => dest.ModifiedBy, o => o.MapFrom(src => src.ModifiedBy))
             .ForMember(dest => dest.DeletedOn, o => o.MapFrom(src => src.DeletedOn))
             .ForMember(dest => dest.DeletedBy, o => o.MapFrom(src => src.DeletedBy))
             .ForMember(dest => dest.IsDeleted, o => o.MapFrom(src => src.IsDeleted))
             .ForMember(dest => dest.User, o => o.MapFrom(src => src.User));


            CreateMap<RequestDto, Request>()
             .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
             .ForMember(dest => dest.Title, o => o.MapFrom(src => src.Title))
             .ForMember(dest => dest.Details, o => o.MapFrom(src => src.Details))
             .ForMember(dest => dest.Notes, o => o.MapFrom(src => src.Notes))
             .ForMember(dest => dest.Status, o => o.MapFrom(src => src.Status))
             .ForMember(dest => dest.Attachments, o => o.MapFrom(src => src.Attachments))
             .ForMember(dest => dest.RequestWorkFlows, o => o.MapFrom(src => src.RequestWorkFlows))
             .ForMember(dest => dest.CreatedOn, o => o.MapFrom(src => src.CreatedOn))
             .ForMember(dest => dest.CreatedBy, o => o.MapFrom(src => src.CreatedBy))
             .ForMember(dest => dest.ModifiedOn, o => o.MapFrom(src => src.ModifiedOn))
             .ForMember(dest => dest.ModifiedBy, o => o.MapFrom(src => src.ModifiedBy))
             .ForMember(dest => dest.DeletedOn, o => o.MapFrom(src => src.DeletedOn))
             .ForMember(dest => dest.DeletedBy, o => o.MapFrom(src => src.DeletedBy))
             .ForMember(dest => dest.IsDeleted, o => o.MapFrom(src => src.IsDeleted))
             .ForMember(dest => dest.User, o => o.MapFrom(src => src.User));
        }
    }
}
