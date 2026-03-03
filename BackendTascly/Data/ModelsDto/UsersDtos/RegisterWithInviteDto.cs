namespace BackendTascly.Data.ModelsDto.UsersDtos
{
    public class RegisterWithInviteDto
    {
        public Guid InviteToken { get; set; }
        public string Username { get; set; } = string.Empty;   // email
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
