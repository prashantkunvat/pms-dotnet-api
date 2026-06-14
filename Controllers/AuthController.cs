using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PMS.Api.DTOs.Auth;
using PMS.Api.Interfaces;

namespace PMS.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterDto dto
    )
    {
        var response = await _service.RegisterAsync(dto);

        return StatusCode(201, new
        {
            message = "User registered successfully",
            data = response
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginDto dto
    )
    {
        var response = await _service.LoginAsync(dto);

        if (response is null)
        {
            return Unauthorized(new
            {
                message = "Invalid credentials"
            });
        }

        return Ok(new
        {
            message = "Login successful",
            data = response
        });
    }
}