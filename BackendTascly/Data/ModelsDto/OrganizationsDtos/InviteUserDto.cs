namespace BackendTascly.Data.ModelsDto.OrganizationsDtos
{
    public class InviteUserDto
    {
        public string Email { get; set; } = string.Empty;
        public bool IsOrgAdmin { get; set; } = false; // true = invite as org admin, false = regular member
    }
}
