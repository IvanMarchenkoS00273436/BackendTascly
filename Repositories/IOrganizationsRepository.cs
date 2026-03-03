using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface IOrganizationsRepository
    {
        Task<Organization> GetOrganization(Guid organizationId);
        Task<bool> UpdateOrganization(Organization organization);
    }
}
