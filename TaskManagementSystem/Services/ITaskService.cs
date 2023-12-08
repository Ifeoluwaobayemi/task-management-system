using Task = TaskManagementSystem.Models.Task;

namespace TaskManagementSystem.Services
{
    public interface ITaskService
    {
        void AddTask(Task task, string assignedUserId);
        void UpdateTask(Task task);
        void DeleteTask(int taskId);
        Task GetTaskById(int taskId);
        IEnumerable<Task> GetAllTasks();

    }
}