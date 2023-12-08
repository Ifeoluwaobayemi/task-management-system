using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Models
{
    public class TaskAssignment
    {
        public int Id { get; set; }

        // Foreign Keys
      
        public int TaskId { get; set; }
        public string AssignedUserId { get; set; }

        // Navigation Properties
        public  Task Task { get; set; }
        public User AssignedUser { get; set; }
    }


}