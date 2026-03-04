using BackendTascly.BusinessLayer;
using BackendTascly.Data;
using BackendTascly.Data.ModelsDto.OrganizationsDtos;
using BackendTascly.Entities;
using BackendTascly.Repositories;
using System.Text;
using System.Text.Json;

namespace BackendTascly.Services
{
    public class OrganizationService(
        IOrganizationsRepository organizationsRepository,
        IInvitationRepository invitationRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration) : IOrganizationService
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

        public async Task<(bool success, string message)> InviteMemberAsync(Guid organizationId, InviteUserDto dto)
        {
            var organization = await organizationsRepository.GetOrganization(organizationId);
            if (organization is null) return (false, "Organization not found.");

            // Create invitation record (the Id IS the token)
            var invitation = new Invitation
            {
                Email = dto.Email,
                OrganizationId = organizationId,
                IsOrgAdmin = dto.IsOrgAdmin,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await invitationRepository.AddInvitationAsync(invitation);

            // Build the invite link
            var frontendUrl = configuration.GetValue<string>("AppSettings:FrontendUrl") ?? "http://localhost:4200";
            var inviteLink = $"{frontendUrl}/register?inviteToken={invitation.Id}";

            // Call Lambda email service
            var lambdaUrl = configuration.GetValue<string>("AppSettings:LambdaEmailUrl");
            if (!string.IsNullOrEmpty(lambdaUrl))
            {
                try
                {
                    var emailPayload = new
                    {
                        email = dto.Email,
                        name = dto.Email,
                        subject = $"You've been invited to join {organization.Name} on Tascly!",
                        body = $"Hi,\n\nYou have been invited to join {organization.Name} on Tascly.\n\nClick the link below to create your account:\n{inviteLink}\n\nThis link expires in 7 days.\n\nTascly Team"
                    };

                    var json = JsonSerializer.Serialize(emailPayload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var client = httpClientFactory.CreateClient("Lambda");
                    await client.PostAsync(lambdaUrl, content);
                }
                catch
                {
                    // Email failed, but invitation was saved — non-fatal
                }
            }

            return (true, $"Invitation sent to {dto.Email}.");
        }
    }
}
