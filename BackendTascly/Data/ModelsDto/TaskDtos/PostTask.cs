using System.ComponentModel.DataAnnotations;

namespace BackendTascly.Data.ModelsDto.TaskDtos
{
    public class PostTask
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; } //optional
        public DateTime DueDate { get; set; } //optional
        public Int16 StatusId { get; set; } //required with default
        public Int16 ImportanceId { get; set; } //required with default
        public Guid? AssigneeId { get; set; }
    }
}
