namespace TaskAPI.Models.Dtos
{
    public class CreateTaskDto
    {

        public string Title { get; set; }
        public string Description { get; set; }

        public string AssignedToId { get; set; }
        public DateTime DueDate { get; set; }

    }
}
