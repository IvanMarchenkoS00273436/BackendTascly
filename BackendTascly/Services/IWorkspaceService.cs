using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Entities;
using System.Threading.Tasks;

namespace BackendTascly.Services
{
    public interface IWorkspaceService
    {
        Task<bool> CreateWorkspaceAsync(Guid userId, PostWorkspaceDto postWorkspaceDto);
        Task<List<Workspace>> GetAllWorkspacesAsync(Guid organizationId);
        Task<Workspace?> GetWorkspaceByIdAsync(Guid workspaceId);
        Task<bool> AddMemberToWorkspaceAsync(PostMemberToWorkspaceDto req, Guid userId, Guid workspaceId);
        Task<List<WorkspaceUserRole>> GetWorkspaceMembers(Guid workspaceId);
        Task<bool> DeleteUserFromWorkspace(Guid userId, Guid workspaceId, Guid memberId);
        Task<bool> UpdateWorkspaceMemberRole(Guid userId, Guid workspaceId, PutMemberWithNewRoleDto putMemberWithNewRoleDto);
        Task<Role?> GetWorkspaceMemberRole(Guid userId, Guid workspaceId);
    }
}
