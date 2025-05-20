using HR.Services.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemHR.Models;

namespace HR.API;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class LoginController(IJwtGenerator jwtGenerator, SystemHRContext context) : ControllerBase
{

    [HttpPost]
    public Task<ActionResult<string>> Login([FromBody] LogowanieModel model)
    {
        var PracownikHR = context.Uzytkownicy.FirstOrDefault(u => u.Email == model.Email);
        if (PracownikHR != null)
        {
            // Weryfikacja hasła
            var hasher = new PasswordHasher<PracownikHR>();
            var wynik = hasher.VerifyHashedPassword(null, PracownikHR.Haslo, model.Haslo);

            if (wynik == PasswordVerificationResult.Success)
            {
                return Task.FromResult<ActionResult<string>>(Ok(jwtGenerator.CreateToken(PracownikHR)));
            }
        }

        return Task.FromResult<ActionResult<string>>(Unauthorized("Nieprawidłowy e-mail lub hasło."));
    }

}