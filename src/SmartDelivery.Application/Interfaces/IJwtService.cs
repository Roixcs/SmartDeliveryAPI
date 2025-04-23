public interface IJwtService
{
    string GenerateToken(string email, string role);

    // Implement the GenerateToken and ExpirationToken method to create a JWT token with the user's email and role
    //string GenerateRefreshToken();
    //ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}