using BackendTascly.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTascly.Data.ModelsDto.TaskDtos
{
    public class GetTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string StatusName { get; set; } //should be retrieved from navigation property Status
        public string ImportanceName { get; set; } //should be retrieved from navigation property Status
        public Guid ProjectId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid AssigneeId { get; set; }
    }
}
