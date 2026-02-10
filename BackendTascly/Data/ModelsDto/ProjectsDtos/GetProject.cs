namespace BackendTascly.Data.ModelsDto.ProjectsDtos
{
    public class GetProject
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid OwnerId { get; set; }
    }
}
