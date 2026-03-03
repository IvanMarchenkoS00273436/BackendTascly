using BackendTascly.Data;
using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Repositories
{
    public class InvitationRepository(TasclyDbContext context) : IInvitationRepository
    {
        public async Task AddInvitationAsync(Invitation invitation)
        {
            await context.Invitations.AddAsync(invitation);
            await context.SaveChangesAsync();
        }

        public async Task<Invitation?> GetByTokenAsync(Guid token)
        {
            return await context.Invitations
                .Include(i => i.Organization)
                .FirstOrDefaultAsync(i => i.Id == token);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
