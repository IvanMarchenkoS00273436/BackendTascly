namespace BackendTascly.Data.ModelsDto.UsersDtos
{
    public class GetUserProfile
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string OrganizationName { get; set; }
    }
}
