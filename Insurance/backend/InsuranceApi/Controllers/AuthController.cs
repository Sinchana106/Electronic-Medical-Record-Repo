using Microsoft.AspNetCore.Mvc;
using Insurance.ApplicationCore.DTOs;
using Insurance.ApplicationCore.Models;
using Insurance.Infrastructure.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private Authservice _authService;
    public AuthController(Authservice authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(User model)
    {
        if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            return BadRequest(new { message = "Invalid registration data" });
        var user = new User
        {
            FullName = model.FullName,
            Email = model.Email,
            Username = model.Username,
            Password = model.Password // In a real application, ensure to hash the password
        };
        try
        {
            var registeredUser = _authService.Register(user).Result; // Use async/await in production code
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while registering the user.", error = ex.Message });
        }
        // Assuming registration was successful


        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDTO model)
    {
        // Validate user and return JWT
        if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            return BadRequest(new { message = "Invalid login data" });
        var isAuthenticated = _authService.Authenticate(model.Username, model.Password).Result; // Use async/await in production code
        if (!isAuthenticated)
            return Unauthorized(new { message = "Invalid username or password" });

        return Ok(new { token = "fake-jwt-token" });
    }
}
