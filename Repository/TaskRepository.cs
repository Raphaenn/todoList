using Microsoft.Data.Sqlite;
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
      
    public async Task<List<TaskModel>> ListAllTasks(string? filterOn = null, string? filterQuery = null, int pageNumber = 1, int pageSize = 2)
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

        int skipResults = (pageNumber - 1) * pageSize;
    
        return await tasks.Skip(skipResults).Take(pageSize).ToListAsync();
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

    public async Task<object> GetTaskByUser()
    {
        string sqlitecommand = "SELECT name FROM tasks";
        string connectionString = "Data Source=/Users/raphaelneves/Developer/c#/TodoListApi/TodoList/TaskSystem.db;";
        List<object> result = new List<object>();
        
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            using (SqliteCommand command = new SqliteCommand(sqlitecommand, connection))
            {
                connection.Open();
                using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                {
                    // Process the results
                    while (reader.Read())
                    {
                        // Access the data using reader["ColumnName"] or reader[index]
                        var column1Value = reader["name"];
                        Console.WriteLine(column1Value);
                        result.Add(column1Value);
                    }
                }
                // In the code you provided, it is generally not necessary to explicitly call connection.Close() due to the use of the using statement.                 The using statement automatically takes care of disposing the resources when the block is exited, including closing the connection.
            }
        }
        return result;
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