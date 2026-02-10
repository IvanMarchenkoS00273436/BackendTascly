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

        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }

        //relationships
        [Required]
        public Int16 StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual PTaskStatus Status { get; set; }

        [Required]
        public Int16 ImportanceId { get; set; }
        [ForeignKey("ImportanceId")]
        public virtual TaskImportance Importance { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        public Guid? AssigneeId { get; set; }
        [ForeignKey("AssigneeId")]
        public virtual User AssignedTo { get; set; }


    }
}