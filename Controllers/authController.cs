using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task.Models.Dtos;
using TodoList.Models.Dtos;
using TodoList.Repository.Interfaces;

namespace TodoList.AuthController
{
    [ApiController]
    [Route("api/[controller]")]
    public class authController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        
        public authController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Registrer([FromBody] UserRegisterDto registerRequestDto)
        {
            Console.WriteLine("testando");
            Console.WriteLine(registerRequestDto.Username, registerRequestDto.Password);
            var user = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await _userManager.CreateAsync(user, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null & registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(user, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        // Create token

                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles != null)
                        {
                           var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());
                           var response = new LoginResponseDto
                           {
                               JwtToken = jwtToken
                           };
                            return Ok(response);
                        }
                    }
                }
            }

            return BadRequest("Something went wrong!");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (checkPasswordResult)
                {
                    // create token
                    
                    
                    return Ok();
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}