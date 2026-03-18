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

            var memberLines = request.Members.Count > 0
                ? string.Join("\n", request.Members.Select(m =>
                    $"  - If user mentions \"{m.FullName.Split(' ')[0]}\" or \"{m.FullName}\" → assigneeId = \"{m.Id}\""))
                : "  - No members. Always use assigneeId: null";

            var systemPrompt =
                "You are a task management AI. Extract tasks from the user input and return ONLY JSON.\n\n" +
                "NAME TO ASSIGNEE LOOKUP (match by first name or full name):\n" +
                memberLines + "\n\n" +
                "JSON FORMAT (respond with this exact structure, no extra text):\n" +
                "{\"tasks\":[{\"name\":\"...\",\"description\":\"...\",\"dueDate\":\"YYYY-MM-DDTHH:mm:ss\",\"startDate\":\"YYYY-MM-DDTHH:mm:ss\",\"importanceId\":1,\"statusId\":1,\"assigneeId\":null}]}\n\n" +
                "RULES:\n" +
                "- importanceId: 1=Low 2=Medium 3=High. statusId: always 1.\n" +
                "- assigneeId: look up the name from the LOOKUP above and use that exact GUID string. If no name mentioned, use JSON null.\n" +
                "- Default dueDate=" + DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-ddTHH:mm:ss") + " startDate=" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + "\n" +
                "- Do NOT return markdown formatting like ```json ... ```. Just the raw JSON object.";

            var payload = new
            {
                model,
                messages = new[]
                {
                        new { role = "system", content = systemPrompt },
                        new { role = "user", content = request.Prompt }
                    },
                response_format = new { type = "json_object" }, // Force JSON mode
                temperature = 0.3, // Low temp for more consistent JSON output
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

            // Safety cleanup: remove markdown code fences if they still appear
            if (messageContent.StartsWith("```"))
            {
                var lines = messageContent.Split('\n').ToList();
                if (lines.First().Trim().StartsWith("```")) lines.RemoveAt(0);
                if (lines.Last().Trim().StartsWith("```")) lines.RemoveAt(lines.Count - 1);
                messageContent = string.Join("\n", lines);
            }

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
            var tasks = request.Tasks.Select(t =>
            {
                Guid? safeAssigneeId = Guid.TryParse(t.AssigneeId, out var parsed) ? parsed : null;
                return new PTask
                {
                    Id = Guid.NewGuid(),
                    Name = t.Name,
                    Description = t.Description ?? string.Empty,
                    StartDate = t.StartDate == default ? now : t.StartDate,
                    DueDate = t.DueDate == default ? now.AddDays(7) : t.DueDate,
                    CreationDate = now,
                    LastModifiedDate = now,
                    StatusId = t.StatusId == 0 ? (short)1 : t.StatusId,
                    ImportanceId = t.ImportanceId == 0 ? (short)1 : t.ImportanceId,
                    ProjectId = request.ProjectId,
                    AuthorId = userId,
                    AssigneeId = safeAssigneeId
                };
            }).ToList();

            return await taskRepository.AddTaskAsync(tasks);
        }
    }
}