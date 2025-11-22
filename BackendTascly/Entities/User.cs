using System.ComponentModel.DataAnnotations;

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
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
