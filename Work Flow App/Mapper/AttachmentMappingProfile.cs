using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using Work_Flow_App.Dtos;

namespace Work_Flow_App.Mapper
{
    public class AttachmentMappingProfile : Profile
    {
        public AttachmentMappingProfile()
        {
            CreateMap<Attachment, AttachmentDto>()
              .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
              .ForMember(dest => dest.FileType, o => o.MapFrom(src => src.FileType))
              .ForMember(dest => dest.Extension, o => o.MapFrom(src => src.Extension))
              .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
              .ForMember(dest => dest.UploadedBy, o => o.MapFrom(src => src.UploadedBy))
              .ForMember(dest => dest.CreatedOn, o => o.MapFrom(src => src.CreatedOn))
              .ForMember(dest => dest.RequestId, o => o.MapFrom(src => src.RequestId))
              .ForMember(dest => dest.Request, o => o.MapFrom(src => src.Request));

            CreateMap<AttachmentDto, Attachment>()
              .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
              .ForMember(dest => dest.FileType, o => o.MapFrom(src => src.FileType))
              .ForMember(dest => dest.Extension, o => o.MapFrom(src => src.Extension))
              .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
              .ForMember(dest => dest.UploadedBy, o => o.MapFrom(src => src.UploadedBy))
              .ForMember(dest => dest.CreatedOn, o => o.MapFrom(src => src.CreatedOn))
              .ForMember(dest => dest.RequestId, o => o.MapFrom(src => src.RequestId))
              .ForMember(dest => dest.Request, o => o.MapFrom(src => src.Request));
        }
    }
}
