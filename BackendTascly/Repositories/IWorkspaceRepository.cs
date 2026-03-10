using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface IWorkspaceRepository
    {
        Task<bool> AddWorkspaceAsync(Workspace workspace);
        Task<List<Workspace>> GetAllWorkspacesAsync(Guid organizationId);
        Task<Workspace?> GetWorkspaceByIdAsync(Guid workspaceId);
        Task<bool> AddMemberToWorkspaceAsync(WorkspaceUserRole workspaceUserRole);
        Task<List<WorkspaceUserRole>> GetWorkspaceMembers(Guid workspaceId);
        Task<Role?> GetWorkspaceUserRoleAsync(Guid userId, Guid workspaceId);
        Task<bool> DeleteUserFromWorkspace(Guid workspaceId, Guid userId);
        Task<bool> UpdateUserRoleInWorkspace(Guid workspaceId, Guid userId, Guid newRoleId);
    }
}
