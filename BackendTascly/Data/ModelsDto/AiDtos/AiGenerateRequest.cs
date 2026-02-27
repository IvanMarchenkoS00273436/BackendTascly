namespace BackendTascly.Data.ModelsDto.AiDtos
{
    public class AiGenerateRequest
    {
        public string Prompt { get; set; }  = string.Empty;
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Mode { get; set; } = "Project"; // Business or Project , default to Project
    }
}
