using TaskAPI.Models.Dtos;

namespace TaskAPI.Services.Interfaces;
public interface ITaskService
{
    Task<ReturnTaskDto> GetTaskByIdAsync(string taskId);
    Task<IEnumerable<ReturnTaskDto>> GetTasksAsync();
    Task<IEnumerable<ReturnTaskDto>> GetTasksByAssignedToIdAsync(string assignedToId);
    Task<IEnumerable<ReturnTaskDto>> GetTasksByAssignedByIdAsync(string assignedById);
    Task<ReturnTaskDto> AddTaskAsync(CreateTaskDto taskDto, string assignedById);
    Task<ReturnTaskDto> UpdateTaskAsync(string taskId, UpdateTaskDto taskDto);
    Task<bool> DeleteTaskAsync(string taskId);
}
