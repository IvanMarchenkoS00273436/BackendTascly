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

            // Mapping between WorkspaceUserRole and GetMemberRoleDto (GetMemberRoleDto from WorkspaceUserRole)
            CreateMap<WorkspaceUserRole, GetMemberRoleDto>()
                .ForMember(dest => dest.MemberId, src => src.MapFrom(x => x.UserId))
                .ForMember(dest => dest.Username, src => src.MapFrom(x => x.User.Username))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.User.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.User.LastName))
                .ForMember(dest => dest.Role, src => src.MapFrom(x => x.Role));
        }
    }
}
