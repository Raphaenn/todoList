using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;
using TodoList.Models.Entity;
using TodoList.Repository.Interfaces;

namespace TodoList.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly TaskSystemDbContext _dbContext;
    
    public TaskRepository(TaskSystemDbContext tasksDbContext)
    {
        _dbContext = tasksDbContext;
    }
    
    public async Task<List<TaskModel>> ListAllTasks(string? filterOn = null, string? filterQuery = null)
    {
        var tasks = _dbContext.Tasks.Include(u => u.User).AsQueryable();
        
        // filter tasks by name
        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                tasks = tasks.Where(x => x.Name.Contains(filterQuery));
            }
        }

        return await tasks.ToListAsync();
    }

    public async Task<TaskModel> GetTaskById(int id)
    {
        return await _dbContext.Tasks.FirstOrDefaultAsync(taskId => taskId.Id == id);
    }

    public async Task<TaskModel> AddTask(TaskModel task)
    {
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();
        return task;
    }

    public async Task<TaskModel> GetTaskByUser(int id)
    {
        TaskModel task = await _dbContext.Tasks.Include(u => u.User).FirstOrDefaultAsync(task => task.UserId ==  id);
        return task;
    }

    public async Task<TaskModel> UpdateTask(TaskModel task, int id)
    {
        TaskModel taskId = await GetTaskById(task.Id);
        if (task is null)
        {
            throw new Exception($"Tarefa com id: {id} não encontrada");
        }

        taskId.Name = task.Name;
        taskId.Description = task.Description;
        taskId.Status = task.Status;
        taskId.UserId = task.UserId;
        await _dbContext.SaveChangesAsync();

        return taskId;
    }

    public async Task<bool> DeleteTask(int id)
    {
        TaskModel taskId = await GetTaskById(id);
        
        if (taskId is null)
        {
            throw new Exception($"Tarefa com id: {id} não encontrada");
        }

        _dbContext.Tasks.Remove(taskId);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}