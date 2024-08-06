using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.API.Models.DTO;
using MyBlog.API.Repositories.Interface;

namespace MyBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            UserManager = userManager;
            TokenRepository = tokenRepository;
        }

        public UserManager<IdentityUser> UserManager { get; }
        public ITokenRepository TokenRepository { get; }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] RegisterRequestDTO request)
        {
            var identityUser = await UserManager.FindByEmailAsync(request.Email);
            if (identityUser != null) 
            { 
                var checkPassword = await UserManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPassword)
                {
                    var Roles = await UserManager.GetRolesAsync(identityUser);
                    var JwtToken = TokenRepository.createJwtToken(identityUser, Roles.ToList());
                    var response = new LoginResponseDTO
                    {

                        Email = request.Email,
                        Roles = Roles.ToList(),
                        Token = JwtToken
                    };
                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Email or Password is Incorrect");
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim()
            };

            var identityresult = await UserManager.CreateAsync(user, request.Password);
            if (identityresult.Succeeded)
            {
                //Add Roles to created user
                identityresult = await UserManager.AddToRoleAsync(user, "Reader");
                if (identityresult.Succeeded)
                {
                    return Ok("sucess");
                }
                else
                {
                    if (identityresult.Errors.Any())
                    {
                        foreach (var error in identityresult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if(identityresult.Errors.Any())
                {
                    foreach(var error in identityresult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }
    }
}
