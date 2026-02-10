using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTascly.Entities
{
    public class Organization
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        //relationships
        //[Required]
        //public Guid OwnerId { get; set; }
        //[ForeignKey("OwnerId")]
        //public virtual User Owner { get; set; }

        public virtual ICollection<User> Members { get; set; } = new List<User>();
        public virtual ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}
