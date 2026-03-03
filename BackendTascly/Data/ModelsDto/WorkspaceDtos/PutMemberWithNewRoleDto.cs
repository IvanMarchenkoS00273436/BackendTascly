using Newtonsoft.Json;

namespace BackendTascly.Data.ModelsDto.WorkspaceDtos
{
    public class PutMemberWithNewRoleDto
    {
        public Guid UserId { get; set; }
        public Guid NewRoleId { get; set; }
    }
}
