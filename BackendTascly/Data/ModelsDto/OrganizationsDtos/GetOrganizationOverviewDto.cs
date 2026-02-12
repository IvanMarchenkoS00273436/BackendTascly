using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.Data.ModelsDto.WorkspaceDtos;

namespace BackendTascly.Data.ModelsDto.OrganizationsDtos
{
    public class GetOrganizationOverviewDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<GetUserDto> Members { get; set; }
        public List<GetWorkspace> Workspaces { get; set; }
        public int TotalTasks { get; set; }
        public int TotalCompletedTasks { get; set; }
        public int TotalToDoTasks { get; set; }
        public int TotalInProgressTasks { get; set; }
    }
}
