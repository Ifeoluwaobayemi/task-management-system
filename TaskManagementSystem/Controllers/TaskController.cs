using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagementSystem.Models;
using TaskManagementSystem.Services;
using Task = TaskManagementSystem.Models.Task;
namespace TaskManagementSystem.Controllers
{

    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            var tasks = _taskService.GetAllTasks();
            return View(tasks);
        }

        public IActionResult Details(int id)
        {
            var task = _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound(); // 404 Not Found
            }

            return View(task);
        }

        public IActionResult Create()
        {
            // Additional logic to prepare data for the create view if needed
            return View();
        }

        [HttpPost]
        public IActionResult Create(Task task, string assignedUserId)
        {
            try
            {
                _taskService.AddTask(task, assignedUserId);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                // Handle the case where the user does not exist
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(task);
            }
        }

        public IActionResult Edit(int id)
        {
            var task = _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound(); // 404 Not Found
            }

            // Additional logic to prepare data for the edit view if needed
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(int id, Task task)
        {
            var existingTask = _taskService.GetTaskById(id);

            if (existingTask == null)
            {
                return NotFound(); // 404 Not Found
            }

            task.Id = id; // Ensure the ID is set correctly
            _taskService.UpdateTask(task);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var task = _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound(); // 404 Not Found
            }

            return View(task);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction(nameof(Index));
        }

        // Other actions as needed...
    }


}
