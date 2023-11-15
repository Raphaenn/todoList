using TodoList.Models;

namespace TodoList.Repository.Interfaces;

public interface IUserRepository
{
    Task<List<UserModel>> ListAllUsers();
    Task<UserModel> GetUsersById(int id);
    Task<UserModel> AddUsers(UserModel users);
    Task<UserModel> UpdateUsers(UserModel users, int id);
    Task<bool> DeleteUsers(int id);
}