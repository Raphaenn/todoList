using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Repository.Interfaces;

namespace TodoList.Controllers
{
    [Route("api/[controller]")] // entre colchete entre o nome da classe "userController" removendo o controler 
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> ListAllUsers()
        {
            List<UserModel> users = await _userRepository.ListAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            UserModel users = await _userRepository.GetUsersById(id);
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser([FromBody] UserModel user)
        {
            UserModel createdUser = await _userRepository.AddUsers(user);
            return Ok(createdUser);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<UserModel>> DeleteUser(int id)
        {
            bool deletedUser = await _userRepository.DeleteUsers(id);
            return Ok(deletedUser);
        }
    }
}