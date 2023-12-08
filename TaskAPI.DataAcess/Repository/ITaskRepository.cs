
using TaskAPI.Models.Model;

namespace TaskAPI.DataAcess.Repository
{
    public interface ITaskRepository
    {
        Task<TaskModel> GetTaskByIdAsync(string taskId);
        Task<IEnumerable<TaskModel>> GetTasksAsync();
        Task<IEnumerable<TaskModel>> GetTasksByAssignedToIdAsync(string assignedToId);
        Task<IEnumerable<TaskModel>> GetTasksByAssignedByIdAsync(string assignedById);
        Task<TaskModel> AddTaskAsync(TaskModel task);
        Task<TaskModel> UpdateTaskAsync(TaskModel task);
        Task<bool> DeleteTaskAsync(string taskId);
    }

}