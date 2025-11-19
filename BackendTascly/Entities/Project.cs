using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTascly.Entities
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        public virtual ICollection<User> Members { get; set; } = new List<User>();
        public virtual ICollection<PTaskStatus> TaskStatuses { get; set; } = new List<PTaskStatus>();
        public virtual ICollection<TaskImportance> TaskImportances { get; set; } = new List<TaskImportance>();
        public virtual ICollection<PTask> Tasks { get; set; } = new List<PTask>();
    }
}
