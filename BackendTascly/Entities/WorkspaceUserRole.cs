using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BackendTascly.Entities
{
    public class WorkspaceUserRole
    {
        [Key, Column(Order = 0)]
        public Guid WorkspaceId { get; set; }

        [Key, Column(Order = 1)]
        public Guid UserId { get; set; }
        
        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("WorkspaceId")]
        public virtual Workspace Workspace { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
