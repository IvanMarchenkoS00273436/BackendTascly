using AutoMapper;
using BackendTascly.Entities;
using BackendTascly.Entities.ModelsDto.ProjectsDtos;

namespace BackendTascly.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, GetProject>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.OwnerId, src => src.MapFrom(x => x.OwnerId));
        }
    }
}
