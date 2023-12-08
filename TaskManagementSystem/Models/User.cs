using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Models
{
    public class User : IdentityUser
    {
        // Additional properties if needed
        public string FullName { get; set; }

        // Navigation Property
        public  ICollection<TaskAssignment> AssignedTasks { get; set; }
    }


}