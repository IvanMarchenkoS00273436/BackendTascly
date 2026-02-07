using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Entities;
using BackendTascly.Repositories;

namespace BackendTascly.Services
{
    public class WorkspaceService(IWorkspaceRepository workspaceRepository, IUsersRepository usersRepository, IRoleRepository roleRepository): IWorkspaceService
    {
        public async Task<bool> CreateWorkspaceAsync(PostWorkspaceDto postWorkspaceDto, Guid userId)
        {
            // find user
            var user = await usersRepository.FindByUserIdAsync(userId);
            if (user is null) return false;

            // create blank workspace
            Workspace workspace = new Workspace();
            workspace.Name = postWorkspaceDto.Name;
            workspace.OrganizationId = user.OrganizationId;

            // add user to the workspace with Admin rights
            WorkspaceUserRole workspaceUserRole = new WorkspaceUserRole();
            var role = await roleRepository.GetAdminRoleAsync();
            workspaceUserRole.User = user;
            workspaceUserRole.Workspace = workspace;
            workspaceUserRole.Role = role;

            workspace.WorkspaceUserRoles.Add(workspaceUserRole);

            return await workspaceRepository.AddWorkspaceAsync(workspace);
        }
    }
}
