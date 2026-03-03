using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface IInvitationRepository
    {
        Task AddInvitationAsync(Invitation invitation);
        Task<Invitation?> GetByTokenAsync(Guid token);
        Task SaveChangesAsync();
    }
}
