using AuthApi.Models;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger, IJwtTokenService jwtTokenService)
    {
        _logger = logger;
        _jwtTokenService = jwtTokenService;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] LoginModel user)
    {
        AuthenticationToken? loginResult = _jwtTokenService.GenerateAuthToken(user);

        return loginResult is null ? Unauthorized() : Ok(loginResult);
    }
}