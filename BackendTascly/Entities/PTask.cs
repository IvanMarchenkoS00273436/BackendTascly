namespace BackendTascly.Entities
{
    public class PTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public TaskImportance Importance { get; set; }
        public User Author { get; set; }
        public List<User> AssignedTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}