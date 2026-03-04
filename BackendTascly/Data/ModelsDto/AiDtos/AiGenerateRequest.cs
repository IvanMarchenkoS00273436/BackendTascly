using System.Text.Json.Serialization;

namespace BackendTascly.Data.ModelsDto.AiDtos
{
    public class AiGenerateRequest
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        [JsonPropertyName("projectId")]
        public Guid ProjectId { get; set; }

        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; } = "Project";

        [JsonPropertyName("members")]
        public List<AiMemberDto> Members { get; set; } = new();
    }

    public class AiMemberDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
    }
}
