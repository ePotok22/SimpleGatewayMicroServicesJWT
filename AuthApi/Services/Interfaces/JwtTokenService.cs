using AuthApi.Models;

namespace AuthApi.Services.Interfaces;

public interface IJwtTokenService
{
    AuthenticationToken? GenerateAuthToken(LoginModel loginModel);
}