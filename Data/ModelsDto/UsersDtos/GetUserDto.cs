using BackendTascly.Entities;

namespace BackendTascly.Data.ModelsDto.UsersDtos
{
    public class GetUserDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSuperAdmin { get; set; }
        
    }
}
