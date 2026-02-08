using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Entities;

namespace BackendTascly.Profiles
{
    public class WorkspaceProfile: Profile
    {
        public WorkspaceProfile()
        {
            // Mapping between Workspace and GetWorkspaceDTO (GetWorkspaceDTO from Workspace)
            CreateMap<Workspace, GetWorkspace>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.OrganizationId, src => src.MapFrom(x => x.OrganizationId));
        }
    }
}
