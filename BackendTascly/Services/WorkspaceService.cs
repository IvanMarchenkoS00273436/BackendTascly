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

        public async Task<List<Workspace>> GetAllWorkspacesAsync(Guid organizationId)
        {
            return await workspaceRepository.GetAllWorkspacesAsync(organizationId);
        }

        public async Task<Workspace?> GetWorkspaceByIdAsync(Guid workspaceId)
        {
            return await workspaceRepository.GetWorkspaceByIdAsync(workspaceId);
        }

        public async Task<bool> AddMemberToWorkspaceAsync(PostMemberToWorkspaceDto req, Guid userId, Guid workspaceId)
        {
            // TODO: check if user who sends the request is Admin within a workspace

            // find a member
            var member = await usersRepository.FindByUserIdAsync(req.MemberId);
            if (member is null) return false;

            // find a workspace
            var workspace = await workspaceRepository.GetWorkspaceByIdAsync(workspaceId);
            if (workspace is null) return false;

            // find a role
            var role = await roleRepository.FindRoleByName(req.RoleName);
            if (role is null) return false;

            // add member to the workspace with requested rights 
            var workspaceUserRole = new WorkspaceUserRole();
            workspaceUserRole.User = member; 
            workspaceUserRole.Workspace = workspace;
            workspaceUserRole.Role = role;

            return await workspaceRepository.AddMemberToWorkspaceAsync(workspaceUserRole);
        }

        public async Task<List<WorkspaceUserRole>> GetWorkspaceMembers(Guid workspaceId)
        {
            return await workspaceRepository.GetWorkspaceMembers(workspaceId);
        }
    }
}
