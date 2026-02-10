namespace BackendTascly.Data.ModelsDto.WorkspaceDtos
{
    public class GetWorkspace
    {
        public Guid Id { get; set; } //temporary: for testing purposes
        public string Name { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
    }
}
