using TodoList.Models;
using TodoList.Models.Entity;

namespace TodoList.Repository.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskModel>> ListAllTasks();
    Task<TaskModel> GetTaskById(int id);
    Task<TaskModel> AddTask(TaskModel task);
    Task<TaskModel> UpdateTask(TaskModel task, int id);
    Task<bool> DeleteTask(int id);
}