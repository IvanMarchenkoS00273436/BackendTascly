using System.ComponentModel.DataAnnotations;

namespace BackendTascly.Data.ModelsDto.ProjectsDtos.StatusesDto
{
    public class PostStatusDto
    {
        [StringLength(100)]
        public required string StatusName { get; set; }
    }
}
