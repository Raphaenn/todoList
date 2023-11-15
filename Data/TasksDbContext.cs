using Microsoft.EntityFrameworkCore;
using TodoList.Data.Maps;
using TodoList.Models;
using TodoList.Models.Entity;

namespace TodoList.Data;

public class TaskSystemDbContext : DbContext
{
    public TaskSystemDbContext(DbContextOptions<TaskSystemDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserModel> Users {get; set; } 
    public DbSet<TaskModel> Tasks {get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsersMap());
        modelBuilder.ApplyConfiguration(new TaskMap());
        base.OnModelCreating(modelBuilder);
    }
    
}