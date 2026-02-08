using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Entities;
using System.Threading.Tasks;

namespace BackendTascly.Services
{
    public interface IWorkspaceService
    {
        Task<bool> CreateWorkspaceAsync(PostWorkspaceDto postWorkspaceDto, Guid organizationId);
        Task<List<Workspace>> GetAllWorkspacesAsync(Guid organizationId);
        Task<Workspace?> GetWorkspaceByIdAsync(Guid workspaceId);
    }
}
