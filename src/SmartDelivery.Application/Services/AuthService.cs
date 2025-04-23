using SmartDelivery.Application.Interfaces;
using SmartDelivery.Contracts.Auth;
using SmartDelivery.Domain.Entities;
using SmartDelivery.Infrastructure.Auth;

namespace SmartDelivery.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        PasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await _userRepository.GetByEmailAsync(request.Email);
        if (existing is not null)
            throw new ApplicationException("Email is already registered.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            PreferredNotification = Enum.Parse<NotificationType>(request.PreferredNotification)
        };

        await _userRepository.AddAsync(user);

        var token = _jwtService.GenerateToken(user);

        return new AuthResponse(user.Id, user.FullName, user.Email, user.PreferredNotification.ToString(), token);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null || !_passwordHasher.Verify(user.PasswordHash, request.Password))
            throw new ApplicationException("Invalid credentials.");

        var token = _jwtService.GenerateToken(user);

        return new AuthResponse(user.Id, user.FullName, user.Email, user.PreferredNotification.ToString(), token);
    }
}
