using Microsoft.AspNetCore.Identity;

namespace MyBlog.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string createJwtToken(IdentityUser user, List<string> roles);
    }
}
