public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);

    //Implement the RefreshTokenAsync method to refresh the JWT token if needed
}