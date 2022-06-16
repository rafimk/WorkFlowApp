using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using Work_Flow_App.Dtos;

namespace Work_Flow_App.Mapper
{
    public class RequestWorkFlowMappingProfile : Profile
    {
        public RequestWorkFlowMappingProfile()
        {
            CreateMap<RequestWorkFlow, RequestWorkFlowDto>()
              .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
              .ForMember(dest => dest.ActionBy, o => o.MapFrom(src => src.ActionBy))
              .ForMember(dest => dest.ActionDate, o => o.MapFrom(src => src.ActionDate))
              .ForMember(dest => dest.Reason, o => o.MapFrom(src => src.Reason))
              .ForMember(dest => dest.Status, o => o.MapFrom(src => src.Status))
              .ForMember(dest => dest.RequestId, o => o.MapFrom(src => src.RequestId))
              .ForMember(dest => dest.User, o => o.MapFrom(src => src.User));

            CreateMap<RequestWorkFlowDto, RequestWorkFlow>()
              .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
              .ForMember(dest => dest.ActionBy, o => o.MapFrom(src => src.ActionBy))
              .ForMember(dest => dest.ActionDate, o => o.MapFrom(src => src.ActionDate))
              .ForMember(dest => dest.Reason, o => o.MapFrom(src => src.Reason))
              .ForMember(dest => dest.Status, o => o.MapFrom(src => src.Status))
              .ForMember(dest => dest.RequestId, o => o.MapFrom(src => src.RequestId))
              .ForMember(dest => dest.User, o => o.MapFrom(src => src.User));
        }
    }
}
