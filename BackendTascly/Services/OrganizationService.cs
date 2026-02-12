using BackendTascly.BusinessLayer;
using BackendTascly.Data;
using BackendTascly.Data.ModelsDto.OrganizationsDtos;
using BackendTascly.Entities;
using BackendTascly.Repositories;

namespace BackendTascly.Services
{
    public class OrganizationService(IOrganizationsRepository organizationsRepository) : IOrganizationService
    {
        public async Task<GetOrganizationOverviewDto> GetOrganizationOverview(Guid organizationId)
        {
            var organization = await organizationsRepository.GetOrganization(organizationId);
            GetOrganizationOverviewDto org = OrganizationBusiness.GetOrganizationOverview(organization);
            return org;
        }

        public async Task<bool> UpdateOrganizationAsync(Guid organizationId, PutOrganization putOrganization)
        {
            Organization organization = await organizationsRepository.GetOrganization(organizationId);
            organization.Name = putOrganization.Name;
            return await organizationsRepository.UpdateOrganization(organization);
        }
    }
}
