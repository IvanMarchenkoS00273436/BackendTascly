using BackendTascly.Data.ModelsDto.AiDtos;
using BackendTascly.Entities;
using BackendTascly.Repositories;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BackendTascly.Services
{
    public class AiService
    (
        IHttpClientFactory httpClientFactory,
        ITaskRepository taskRepository,
        IConfiguration configuration) : IAiService
    {
        // Calls Groq's OpenAI-compatible API to generate tasks based on a prompt and project context
        // Returns a list of generated tasks as DTOs


        public async Task<AiGenerateResponse> GenerateTasksAsync(AiGenerateRequest request)
        {
            var apiKey = configuration["Groq:ApiKey"];
            var model = configuration["Groq:Model"] ?? "llama-3.3-70b-versatile";

            var client = httpClientFactory.CreateClient("Groq");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // Build a strict system prompt so the AI always returns parseable json
            var systemPrompt = $$""" 
                You are a task managemnet AI assistant working in {{request.Mode}} mode.
                Your Job is to extract actionable tasks from the user's text.
                
                You MUST respond with ONLY valid JSON in this exact format, no other text:
                {
                "tasks": [
                  {
                   "name"  "Task name (max 100 chars),
                   "description": "Task description",
                   "dueDate": "YYYY-MM-DDTHH:mm:ss",
                   "startDate": "YYYY-MM-DDTHH:mm:ss",
                   "importanceId": 1,
                   "statusId": 1,
                  }
                 ]
                }

                importanceId: 1=Low, 2=Medium, 3=High
                statusId: always 1 (pending) for new tasks
                If no due date is mentiuoned, set dueDate to 7 days from today.
                If no start date is mentioned, set startDate to today.
                Today's date is {{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ss}}. 
                """;

            var payload = new
            {
                model,
                messages = new[]
                {
                        new { role = "system", content = systemPrompt },
                        new { role = "user", content = request.Prompt }
                    },
                temperature = 0.3, // Low temp for more consistent JSON outpt
                max_tokens = 2048
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new Exception($"Groq API error: {response.StatusCode}, {errorBody}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();

            //Parse the groq response wrapper, extract the AI message content
            using var doc = JsonDocument.Parse(responseJson);
            var messageContent = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? "{}";

            // Parse the AI's Json response into our DTO
            var aiResponse = JsonSerializer.Deserialize<AiGenerateResponse>(
                messageContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? new AiGenerateResponse();

            return aiResponse;

        }

        // Saves a list of approved AI-generated tasks to the database.
        public async Task<bool> BulkCreateTasksAsync(BulkCreateRequest request, Guid userId)
        {
            if (request.Tasks == null || request.Tasks.Count == 0)
                return false;

            var now = DateTime.UtcNow;
            var tasks = request.Tasks.Select(t => new PTask
            {
                Id = Guid.NewGuid(),
                Name = t.Name,
                Description = t.Description ?? string.Empty,
                StartDate = t.StartDate == default ? now : t.StartDate,
                DueDate = t.DueDate == default ? now.AddDays(7) : t.DueDate,
                CreationDate = now,
                LastModifiedDate = now,
                StatusId = t.StatusId == 0 ? (short)1 : t.StatusId, // Default to pending
                ImportanceId = t.ImportanceId == 0 ? (short)1 : t.ImportanceId, // Default to Low
                ProjectId = request.ProjectId,
                AuthorId = userId,
                AssigneeId = t.AssigneeId
            }).ToList();

            return await taskRepository.AddTaskAsync(tasks);
        }
    }
}
       
           