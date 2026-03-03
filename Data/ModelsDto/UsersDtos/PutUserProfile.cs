namespace BackendTascly.Data.ModelsDto.UsersDtos
{
    public class PutUserProfile
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
    }
}
