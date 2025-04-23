public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _JWTokenService;


    public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _JWTokenService = jwtokenService;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.UserRepository.GetByEmailAsync(request.Email);
        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        // if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        // {
        //     throw new UnauthorizedAccessException("Invalid email or password.");
        // }

        return _JWTokenService.GenerateToken(user);
    }
}