using Microsoft.AspNetCore.Identity;

namespace TodoList.Repository.Interfaces;

public interface ITokenRepository
{
    string CreateJwtToken(IdentityUser user, List<string> roles);
}