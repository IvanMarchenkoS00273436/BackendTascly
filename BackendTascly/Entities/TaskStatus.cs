using System.ComponentModel.DataAnnotations;

namespace BackendTascly.Entities
{
    public class TaskStatus
    {
        [Key]
        public Int16 Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
