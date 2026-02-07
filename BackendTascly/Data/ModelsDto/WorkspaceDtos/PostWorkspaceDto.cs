using System.ComponentModel.DataAnnotations;

namespace BackendTascly.Data.ModelsDto.WorkspaceDtos
{
    public class PostWorkspaceDto
    {
        [Required(ErrorMessage = "Workspace Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}
