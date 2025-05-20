using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SystemHR.Models;

namespace SystemHR.ApiControllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly SystemHRContext _context;
    private readonly IPasswordHasher<PracownikHR> _hasher;

    public AuthController(SystemHRContext context,
                          IPasswordHasher<PracownikHR> hasher)
    {
        _context = context;
        _hasher = hasher;
    }

    // POST api/auth/login
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LogowanieViewModel model)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var user = _context.Uzytkownicy
                           .FirstOrDefault(u => u.Email == model.Email);
        if (user is null)
            return Unauthorized();

        var result = _hasher.VerifyHashedPassword(user,
                                                  user.Haslo,
                                                  model.Haslo);
        if (result != PasswordVerificationResult.Success)
            return Unauthorized();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("Imie",         user.Imie),
            new Claim("Nazwisko",     user.Nazwisko),
            new Claim("Uprawnienia", (user.Id_Uprawnienia ?? 3).ToString())
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(
                new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme)));

        return Ok();
    }

    // POST api/auth/logout
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }
}
