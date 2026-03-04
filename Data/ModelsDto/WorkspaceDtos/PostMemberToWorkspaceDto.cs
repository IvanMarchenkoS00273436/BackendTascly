namespace BackendTascly.Data.ModelsDto.WorkspaceDtos
{
    public class PostMemberToWorkspaceDto
    {
        public Guid MemberId { get; set; }
        public string RoleName { get; set; } = string.Empty; // may later change to RoleID
    }
}
