using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTascly.Entities
{
    public class PTask
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public Int16 StatusId { get; set; }
        public virtual TaskStatus Status { get; set; }

        [Required]
        public Int16 ImportanceId { get; set; }
        public virtual TaskImportance Importance { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        
        [Required]
        public Guid AuthorId { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<User> AssignedTo { get; set; } = new List<User>();

        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
    }
}