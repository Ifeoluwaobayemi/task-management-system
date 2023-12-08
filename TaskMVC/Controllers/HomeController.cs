using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskMVC.Models;
using TaskMVC.Service;

namespace TaskMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TaskService _taskService;
        public HomeController(ILogger<HomeController> logger, TaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        // GET: /Task/Index
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTasksAsync();

            // Pass the tasks to the view
            return View(tasks);
        }

        // GET: /Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTaskDto taskDto)
        {
            if (ModelState.IsValid)
            {
                // Call the service to create a new task
                var createdTask = await _taskService.Create(taskDto);

                if (createdTask != null)
                {
                    // Redirect to the index page after successful creation
                    return RedirectToAction(nameof(Index));
                }
            }

            // If the model is not valid, return to the create page with errors
            return View(taskDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
