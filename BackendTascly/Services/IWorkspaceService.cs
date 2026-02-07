using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Entities;

namespace BackendTascly.Services
{
    public interface IWorkspaceService
    {
        Task<bool> CreateWorkspaceAsync(PostWorkspaceDto postWorkspaceDto, Guid organizationId);
    }
}
