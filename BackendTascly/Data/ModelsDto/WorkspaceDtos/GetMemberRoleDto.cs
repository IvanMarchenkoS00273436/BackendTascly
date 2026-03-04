using BackendTascly.Entities;

namespace BackendTascly.Data.ModelsDto.WorkspaceDtos
{
    public class GetMemberRoleDto
    {
        public Guid MemberId { get; set; }
        public Guid Id => MemberId;   // Frontend expects 'id'
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }
}
