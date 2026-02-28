using BackendTascly.Data.ModelsDto.OrganizationsDtos;
using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Entities;

namespace BackendTascly.BusinessLayer
{
    public static class OrganizationBusiness
    {
        public static GetOrganizationOverviewDto GetOrganizationOverview(Organization organization)
        {
            GetOrganizationOverviewDto overviewDto = new GetOrganizationOverviewDto()
            {
                Id = organization.Id.ToString(),
                Name = organization.Name,
                Members = organization.Members.Select(m => new GetUserDto()
                {
                    UserId = Guid.Parse(m.Id.ToString()),
                    Username = m.Username,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    IsSuperAdmin = m.IsSuperAdmin,
                }).ToList(),
                Workspaces = organization.Workspaces.Select(w => new GetWorkspace()
                {
                    Id = Guid.Parse(w.Id.ToString()),
                    Name = w.Name,
                    OrganizationId = Guid.Parse(w.OrganizationId.ToString()),

                }).ToList(),
                TotalTasks = organization.Workspaces.Sum(w => w.Projects.Sum(p => p.Tasks.Count)),
                TotalInProgressTasks = organization.Workspaces.Sum(w => w.Projects.Sum(p => p.Tasks.Count(t => t.Status != null && t.Status.Name == "In Progress"))),
                TotalCompletedTasks = organization.Workspaces.Sum(w => w.Projects.Sum(p => p.Tasks.Count(t => t.Status != null && t.Status.Name == "Completed"))),
                TotalToDoTasks = organization.Workspaces.Sum(w => w.Projects.Sum(p => p.Tasks.Count(t => t.Status != null && t.Status.Name == "To Do")))
            };

            return overviewDto;
        }
    }
}
