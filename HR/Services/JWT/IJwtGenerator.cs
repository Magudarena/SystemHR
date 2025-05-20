using SystemHR.Models;

namespace HR.Services.JWT;

public interface IJwtGenerator
{
    string CreateToken(PracownikHR user);
    // RefreshToken GenerateRefreshToken();
}