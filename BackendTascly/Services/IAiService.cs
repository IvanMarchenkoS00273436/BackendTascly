using BackendTascly.Data.ModelsDto.AiDtos;
namespace BackendTascly.Services
{
    public interface IAiService
    {
        Task<AiGenerateResponse> GenerateTasksAsync(AiGenerateRequest request);
        Task<bool> BulkCreateTasksAsync(BulkCreateRequest request, Guid userId);
    }
}
