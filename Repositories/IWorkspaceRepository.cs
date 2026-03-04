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
    }
}
