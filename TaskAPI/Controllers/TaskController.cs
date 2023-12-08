using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models.Dtos;
using TaskAPI.Models.Model;
using TaskAPI.Services.Interfaces;

namespace TaskAPI.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize] // Add this attribute if you want to secure the endpoints
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly UserManager<ApplicationUser> _userManager;
    public TaskController(ITaskService taskService, UserManager<ApplicationUser> userManager)
    {
        _taskService = taskService;
        _userManager = userManager;
    }

    [HttpGet("{taskId}")]
    public async Task<ActionResult<ReturnTaskDto>> GetTaskById(string taskId)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReturnTaskDto>>> GetTasks()
    {
        var tasks = await _taskService.GetTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("assigned-to/{assignedToId}")]
    public async Task<ActionResult<IEnumerable<ReturnTaskDto>>> GetTasksByAssignedToId(string assignedToId)
    {
        var tasks = await _taskService.GetTasksByAssignedToIdAsync(assignedToId);
        return Ok(tasks);
    }

    [HttpGet("assigned-by/{assignedById}")]
    public async Task<ActionResult<IEnumerable<ReturnTaskDto>>> GetTasksByAssignedById(string assignedById)
    {
        var tasks = await _taskService.GetTasksByAssignedByIdAsync(assignedById);
        return Ok(tasks);
    }

    [HttpPost] 
    public async Task<ActionResult<ReturnTaskDto>> CreateTask(CreateTaskDto taskDto)
    {
       
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var createdTask = await _taskService.AddTaskAsync(taskDto, userId);
        if (createdTask == null)
        {
            return BadRequest("failed to create task");
        }

        return CreatedAtAction(nameof(GetTaskById), new { taskId = createdTask.TaskId }, createdTask);
    }


    [HttpPut("{taskId}")]
    public async Task<ActionResult<ReturnTaskDto>> UpdateTask(string taskId, UpdateTaskDto taskDto)
    {
        var  updateToUser = await _userManager.FindByIdAsync(taskDto.AssignedToId);
        if(updateToUser == null)
        {
            return NotFound("AssignedTo user not found");
        }
        
        var updatedTask = await _taskService.UpdateTaskAsync(taskId, taskDto);

        if (updatedTask == null)
        {
            return NotFound();
        }

        return Ok(updatedTask);
    }

    [HttpDelete("{taskId}")]
    public async Task<ActionResult> DeleteTask(string taskId)
    {
        var result = await _taskService.DeleteTaskAsync(taskId);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
