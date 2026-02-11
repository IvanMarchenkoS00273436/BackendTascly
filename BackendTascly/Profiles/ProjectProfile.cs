using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Entities;

namespace BackendTascly.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            // Mapping between Project and GetProject DTO (GetProject from Project)
            CreateMap<Project, GetProject>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.OwnerId, src => src.MapFrom(x => x.OwnerId))
                .ForMember(dest => dest.WorkspaceId, src => src.MapFrom(x => x.WorkspaceId));

            // Mapping between PostProject DTO and Project (Project from PostProject)
            CreateMap<PostProject, Project>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description));
        }
    }
}
