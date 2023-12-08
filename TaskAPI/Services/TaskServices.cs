using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAPI.DataAcess.Repository;
using TaskAPI.Models.Dtos;
using TaskAPI.Models.Model;
using TaskAPI.Services.Interfaces;


namespace TaskAPI.Services;
public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ReturnTaskDto> GetTaskByIdAsync(string taskId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);
        return MapTaskModelToReturnTaskDto(task);
    }

    public async Task<IEnumerable<ReturnTaskDto>> GetTasksAsync()
    {
        var tasks = await _taskRepository.GetTasksAsync();
        return MapTaskModelsToReturnTaskDtos(tasks);
    }

    public async Task<IEnumerable<ReturnTaskDto>> GetTasksByAssignedToIdAsync(string assignedToId)
    {

        var tasks = await _taskRepository.GetTasksByAssignedToIdAsync(assignedToId);

        return MapTaskModelsToReturnTaskDtos(tasks);
    }

    public async Task<IEnumerable<ReturnTaskDto>> GetTasksByAssignedByIdAsync(string assignedById)
    {
        var tasks = await _taskRepository.GetTasksByAssignedByIdAsync(assignedById);
        return MapTaskModelsToReturnTaskDtos(tasks);
    }

    public async Task<ReturnTaskDto> AddTaskAsync(CreateTaskDto taskDto, string assignedById)
    {
        var taskModel = new TaskModel
        {
            TaskId = Guid.NewGuid().ToString(),
            Title = taskDto.Title,
            Description = taskDto.Description,
            DueDate = taskDto.DueDate,
            AssignedById = assignedById,
            AssignedToId = taskDto.AssignedToId
        };

        var createdTask = await _taskRepository.AddTaskAsync(taskModel);
        return MapTaskModelToReturnTaskDto(createdTask);
    }

    public async Task<ReturnTaskDto> UpdateTaskAsync(string taskId, UpdateTaskDto taskDto)
    {
        var existingTask = await _taskRepository.GetTaskByIdAsync(taskId);

        
        if (existingTask != null)
        {
            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.DueDate = taskDto.DueDate;
            existingTask.AssignedToId = taskDto.AssignedToId;

            var updatedTask = await _taskRepository.UpdateTaskAsync(existingTask);
            return MapTaskModelToReturnTaskDto(updatedTask);
        }

        return null;
    }

    public async Task<bool> DeleteTaskAsync(string taskId)
    {
        return await _taskRepository.DeleteTaskAsync(taskId);
    }

    private ReturnTaskDto MapTaskModelToReturnTaskDto(TaskModel task)
    {
        if (task == null)
            return null;

        return new ReturnTaskDto
        {
            TaskId = task.TaskId,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            AssignedById = task.AssignedById,
            AssignedToId = task.AssignedToId
        };
    }

    private IEnumerable<ReturnTaskDto> MapTaskModelsToReturnTaskDtos(IEnumerable<TaskModel> tasks)
    {
        return tasks.Select(MapTaskModelToReturnTaskDto);
    }
}
