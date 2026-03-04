namespace BackendTascly.Data.ModelsDto.AiDtos
{
    public class BulkCreateRequest
    {
        public List <AiTaskDto> Tasks { get; set; } = new();
        public Guid ProjectId { get; set; }
        
    }
}
