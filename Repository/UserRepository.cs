using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Repository.Interfaces;

namespace TodoList.Models.Repository;

public class UserRepository : IUserRepository
{
    private readonly TaskSystemDbContext _dbContext;
    
    public UserRepository(TaskSystemDbContext tasksDbContext)
    {
        _dbContext = tasksDbContext;
    }
    
    public async Task<List<UserModel>> ListAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<UserModel> GetUsersById(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(userId => userId.Id == id);
    }

    public async Task<UserModel> AddUsers(UserModel users)
    {
        await _dbContext.Users.AddAsync(users);
        await _dbContext.SaveChangesAsync();
        return users;
    }

    public async Task<UserModel> UpdateUsers(UserModel users, int id)
    {
        UserModel userId = await GetUsersById(users.Id);
        if (userId is null)
        {
            throw new Exception($"Usuário com id: {id} não encontrado");
        }

        userId.Name = users.Name;
        userId.Email = users.Email;
        await _dbContext.SaveChangesAsync();

        return userId;
    }

    public async Task<bool> DeleteUsers(int id)
    {
        UserModel userId = await GetUsersById(id);
        
        if (userId is null)
        {
            throw new Exception($"Usuário com id: {id} não encontrado");
        }

        _dbContext.Users.Remove(userId);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}