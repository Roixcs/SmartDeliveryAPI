public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    //private readonly IEmailService _emailService;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        //_emailService = emailService;
    }

    public async Task<Guid> handler ( RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var ifExist = await _userRepository.GetEmailAsync(request.Email, cancellationToken);
        if(ifExist != null)
        {
            throw new BadRequestException("Email already exists");
        }
        
        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            NotificationPreference = request.NotifiPreference
        };

        await _userRepository.AddAsync(user, cancellationToken);
        //await _emailService.SendWelcomeEmailAsync(user.Email);

        return user.Id;
    }
}