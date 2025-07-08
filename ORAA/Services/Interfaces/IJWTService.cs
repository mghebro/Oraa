using ORAA.Core;
using ORAA.Models;

namespace ORAA.Services.Interfaces;

public interface IJWTService
{
    UserToken GetUserToken(User user);
    string GenerateRefreshToken();

    public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token,
        DateTime expiration);
}
