using BackendTascly.Data.ModelsDto.OrganizationsDtos;

namespace BackendTascly.Services
{
    public interface IOrganizationService
    {
        public Task<GetOrganizationOverviewDto> GetOrganizationOverview(Guid organizationId);
        public Task<bool> UpdateOrganizationAsync(Guid organizationId, PutOrganization putOrganization);
    }
}
