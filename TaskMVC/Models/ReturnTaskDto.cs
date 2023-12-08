namespace TaskMVC.Models
{
    public class ReturnTaskDto
    {
        public string TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string AssignedById { get; set; }
        public string AssignedToId { get; set; }

    }
}