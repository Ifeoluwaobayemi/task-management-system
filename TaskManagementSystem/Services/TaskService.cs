using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Models;
using TaskManagementSystem.Repository;
using Task = TaskManagementSystem.Models.Task;

namespace TaskManagementSystem.Services
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<User> _userManager;

        public TaskService(ITaskRepository taskRepository, UserManager<User> userManager)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public Task GetTaskById(int taskId)
        {
            return _taskRepository.GetTaskById(taskId);
        }

        public void AddTask(Task task, string assignedUserId)
        {
            // Validate and set the assigned user
            var assignedUser = _userManager.FindByIdAsync(assignedUserId).Result;
            if (assignedUser == null)
            {
                // Handle the case where the user does not exist
                throw new InvalidOperationException("Invalid user ID");
            }

            task.AssignedUserId = assignedUserId;
            _taskRepository.AddTask(task);
        }

        public void UpdateTask(Task task)
        {
            // Additional validation if needed
            _taskRepository.UpdateTask(task);
        }

        public void DeleteTask(int taskId)
        {
            // Additional validation if needed
            _taskRepository.DeleteTask(taskId);
        }

        // Other methods as needed...
    }

}
