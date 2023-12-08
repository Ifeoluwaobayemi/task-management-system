namespace TaskManagementSystem.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        // Foreign Key
        public string AssignedUserId { get; set; }

        // Navigation Property
        public  User AssignedUser { get; set; }

    
    }

}
