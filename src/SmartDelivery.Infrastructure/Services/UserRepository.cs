public class UserRepository : IUserRepository
{
    private readonly SmartDeliveryDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public UserRepository(SmartDeliveryDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterUserAsync(string fullName, string email, string password, NotificationPreference notificationPreference)
    {
        var hashedPassword = _passwordHasher.HashPassword(password);
        var user = new User
        {
            FullName = fullName,
            Email = email,
            PasswordHash = hashedPassword,
            NotificationPreference = notificationPreference
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }
}