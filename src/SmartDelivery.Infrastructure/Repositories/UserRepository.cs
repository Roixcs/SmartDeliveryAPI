using Microsoft.EntityFrameworkCore;
using SmartDelivery.Application.Interfaces;
using SmartDelivery.Domain.Entities;
using SmartDelivery.Infrastructure.Persistence;

namespace SmartDelivery.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SmartDeliveryDbContext _smartDeliveryDbContext;
    private readonly IPasswordHasher _passwordHasher;

    public UserRepository(SmartDeliveryDbContext smartDeliveryDbContext, IPasswordHasher passwordHasher)
    {
        _smartDeliveryDbContext = smartDeliveryDbContext;
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

        await _smartDeliveryDbContext.Users.AddAsync(user);
        await _smartDeliveryDbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _smartDeliveryDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _smartDeliveryDbContext.Users.AddAsync(user);
        await _smartDeliveryDbContext.SaveChangesAsync();
    }
}