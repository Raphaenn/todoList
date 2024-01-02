using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models.Dtos;
using TodoList.Models.Entity;
using TodoList.Repository.Interfaces;

namespace TodoList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskRepository taskRepository, IMapper mapper, ILogger<TasksController> logger)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<TaskModel>>> ListAllTasks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 2)
    {
        _logger.LogInformation("Get all tasks was invoked");
        // _logger.LogError("Get all tasks was invoked");
        
        List<TaskModel> tasks = await _taskRepository.ListAllTasks(filterOn, filterQuery, pageNumber, pageSize);
        
        _logger.LogInformation($"Data base tasks: {JsonSerializer.Serialize(tasks)}");
        return Ok(tasks);
    }

    [HttpGet("/{id}")]
    [Authorize]
    public async Task<ActionResult<TaskModel>> GetTaskById(int id)
    {
        // // Get data from data base
        TaskModel task = await _taskRepository.GetTaskById(id);
        
        
        // return Dtos
        // var res = new TaskDTO
        // {
        //     Nome = task.Name,
        //     Descrição = task.Description
        // };
        
        return Ok(_mapper.Map<TaskDto>(task));
    }

    [HttpPost("/api/create")]
    [Authorize]
    public async Task<ActionResult<TaskModel>> CreateTask([FromBody] TaskModel task)
    {
        TaskModel createdTask = await _taskRepository.AddTask(task);
        return Ok(createdTask);
    }

    [HttpPut("/api/update/{id}")]
    [Authorize]
    public async Task<ActionResult<TaskModel>> UpdateTask([FromBody] TaskModel task, int id)
    {
        TaskModel updatedTask = await _taskRepository.UpdateTask(task, id);

        var res = new TaskDto
        {
            Nome = updatedTask.Name,
            Descrição = updatedTask.Description
        };
        return Ok(res);
    }

    [HttpGet("/api/sqlite/query")]
    [Authorize]
    public async Task<ActionResult> GetUsingSql()
    {
        var res = await _taskRepository.GetTaskByUser();
        return Ok(res);
    } 
}