using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodayWebApi.BLL.Dtos;
using TodayWebApi.BLL.Managers;
using TodayWebAPi.DAL.Data.Identity;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenManager _tokenManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenManager tokenManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenManager = tokenManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }
        var token = await _tokenManager.CreateToken(user);

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = token
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
        {
            return BadRequest("Email is already taken.");
        }

        var user = new User
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var token = await _tokenManager.CreateToken(user);

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = token
        };
    }

    [HttpGet("currentUser")]
    [Authorize]  
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
            return Unauthorized("User not authenticated");

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return NotFound("User not found");
        var token = await _tokenManager.CreateToken(user);

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = token
        };
    }

    [HttpGet("checkEmail")]
    public async Task<ActionResult<bool>> CheckExistingEmail([FromQuery] string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }
}
