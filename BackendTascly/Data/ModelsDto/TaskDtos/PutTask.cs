using System.ComponentModel.DataAnnotations;

namespace BackendTascly.Data.ModelsDto.TaskDtos
{
    public class PutTask
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; } 
        public Int16 StatusId { get; set; } 
        public Int16 ImportanceId { get; set; }
        public Guid? AssigneeId { get; set; }
    }
}
