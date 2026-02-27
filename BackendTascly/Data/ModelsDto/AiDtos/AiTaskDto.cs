namespace BackendTascly.Data.ModelsDto.AiDtos
{
    public class AiTaskDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public short ImportanceId { get; set; }
        public short StatusId { get; set; }
        public Guid? AssigneeId { get; set; }
    }
}
