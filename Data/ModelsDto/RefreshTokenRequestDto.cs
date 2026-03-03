namespace BackendTascly.Data.ModelsDto
{
    public class RefreshTokenRequestDto
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
