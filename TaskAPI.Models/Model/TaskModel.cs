using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAPI.Models.Model
{
    public class TaskModel
    {
        [Key]
        public string TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }



        [ForeignKey("AssignedBy")]
        public string AssignedById { get; set; }

        [ForeignKey("AssignedTo")]
        public string AssignedToId { get; set; }

        public ApplicationUser AssignedBy { get; set; }
        public ApplicationUser AssignedTo { get; set; }
    }

}
