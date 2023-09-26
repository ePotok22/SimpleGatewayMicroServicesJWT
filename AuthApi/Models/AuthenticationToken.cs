namespace AuthApi.Models;
public record AuthenticationToken(string Token, int ExpiresIn);