using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTascly.Entities
{
    public class Invitation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public Guid OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        public bool IsOrgAdmin { get; set; } = false; // role granted on registration

        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(7);

        public bool IsUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
