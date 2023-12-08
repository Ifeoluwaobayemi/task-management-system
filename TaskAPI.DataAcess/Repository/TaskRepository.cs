using Microsoft.EntityFrameworkCore;
using TaskAPI.DataAcess.Data;
using TaskAPI.Models.Model;


namespace TaskAPI.DataAcess.Repository
{

    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskModel> GetTaskByIdAsync(string taskId)
        {

            return await _context.Tasks
                .Include(t => t.AssignedBy)
                .Include(t => t.AssignedTo)
                .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task<IEnumerable<TaskModel>> GetTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.AssignedBy)
                .Include(t => t.AssignedTo)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByAssignedToIdAsync(string assignedToId)
        {
            var assignedToUser = await _context.Users.FindAsync(assignedToId);
            if (assignedToUser != null)
            {

                return await _context.Tasks
                    .Where(t => t.AssignedToId == assignedToId)
                    .Include(t => t.AssignedBy)
                    .ToListAsync();
            }
            else
            {
                return null;
            }
        }


        public async Task<IEnumerable<TaskModel>> GetTasksByAssignedByIdAsync(string assignedById)
        {
            return await _context.Tasks
                .Where(t => t.AssignedById == assignedById)
                .Include(t => t.AssignedTo)
                .ToListAsync();
        }

        public async Task<TaskModel> AddTaskAsync(TaskModel task)
        {
            var assignedToUser = await _context.Users.FindAsync(task.AssignedToId);
            if (assignedToUser != null)
            {

                var createdTask = _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return createdTask.Entity;
            }
            else
            {
                return null;
            }

        }

        public async Task<TaskModel> UpdateTaskAsync(TaskModel task)
        {
            var existingTask = await _context.Tasks
                .FirstOrDefaultAsync(t => t.TaskId == task.TaskId);

            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.AssignedById = task.AssignedById;
                existingTask.AssignedToId = task.AssignedToId;

                await _context.SaveChangesAsync();

                return existingTask;
            }

            return null;
        }

        public async Task<bool> DeleteTaskAsync(string taskId)
        {
            var taskToDelete = await _context.Tasks
                .FirstOrDefaultAsync(t => t.TaskId == taskId);

            if (taskToDelete != null)
            {
                _context.Tasks.Remove(taskToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

}
