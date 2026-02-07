using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface IWorkspaceRepository
    {
        Task<bool> AddWorkspaceAsync(Workspace workspace);
    }
}
