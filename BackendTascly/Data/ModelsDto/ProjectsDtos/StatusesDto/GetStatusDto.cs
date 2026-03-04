namespace BackendTascly.Data.ModelsDto.ProjectsDtos.StatusesDto
{
    public class GetStatusDto
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }
        public Int16? NextStatusId { get; set; }
        public Guid ProjectId { get; set; }
    }
}

