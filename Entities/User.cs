using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTascly.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required, EmailAddress]
        public string Username { get; set; } = string.Empty;

        [StringLength(100), MinLength(2)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100), MinLength(2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public bool IsSuperAdmin { get; set; } = false;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        //relationships
        [Required]
        public Guid OrganizationId { get; set; } //1
        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        public virtual ICollection<WorkspaceUserRole> WorkspaceUserRoles { get; } = new List<WorkspaceUserRole>();
        public virtual ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}
