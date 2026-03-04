using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTascly.Entities
{
    public class PTaskStatus
    {
        [Key]
        public Int16 Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public Int16? NextStatusId { get; set; }
        [ForeignKey("NextStatusId")]
        public virtual PTaskStatus? NextStatus { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
