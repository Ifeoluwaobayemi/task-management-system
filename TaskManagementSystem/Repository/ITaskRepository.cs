   using Task = TaskManagementSystem.Models.Task;



namespace TaskManagementSystem.Repository
{
    public interface ITaskRepository
    {
        void AddTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(int taskId);
        Task GetTaskById(int taskId);
        IEnumerable<Task> GetAllTasks();
      

    }
}