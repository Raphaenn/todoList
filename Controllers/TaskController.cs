using AutoMapper;
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

    public TasksController(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskModel>>> ListAllTasks()
    {
        List<TaskModel> tasks = await _taskRepository.ListAllTasks();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskModel>> GetTaskById(int id)
    {
        // // Get data from data base
        TaskModel task = await _taskRepository.GetTaskById(id);
        
        
        // // return Dtos
        // var res = new TaskDTO
        // {
        //     Nome = task.Name,
        //     Descrição = task.Description
        // };
        
        return Ok(_mapper.Map<TaskDto>(task));
    }

    [HttpPost("/api/create")]
    public async Task<ActionResult<TaskModel>> CreateTask([FromBody] TaskModel task)
    {
        TaskModel createdTask = await _taskRepository.AddTask(task);
        return Ok(createdTask);
    }

    [HttpPut("/api/update/{id}")]
    public async Task<ActionResult<TaskModel>> UpdateTask([FromBody] TaskModel task, int id)
    {
        TaskModel updatedTask = await _taskRepository.UpdateTask(task, id);

        var res = new TaskDto
        {
            Name = updatedTask.Name,
            Description = updatedTask.Description
        };
        return Ok(res);
    }
}