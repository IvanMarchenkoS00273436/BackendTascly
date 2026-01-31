using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTascly.Entities
{
    public class Workspace
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
        
        //relationships
        [Required]
        public Guid OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        //[Required]
        //public Guid OwnerId { get; set; }
        //[ForeignKey("OwnerId")]
        //public virtual User Owner { get; set; }

        public virtual ICollection<WorkspaceUserRole> WorkspaceUserRoles { get; } = new List<WorkspaceUserRole>();
        //public virtual ICollection<User> Members { get; set; } = new List<User>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
