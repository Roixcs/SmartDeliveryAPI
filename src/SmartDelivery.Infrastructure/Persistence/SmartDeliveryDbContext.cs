using Microsoft.EntityFrameworkCore;
using SmartDeliviry.Domain.Entities;

namespace SmartDelivery.Infrastructure.Persistence;

public class SmartDeliveryDbContext : DbContext
{
    public SmartDeliveryDbContext(DbContextOptions<SmartDeliveryDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    //public DbSet<NotificationPreference> NotificationPreferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartDeliveryDbContext).Assembly);

        // base.OnModelCreating(modelBuilder);

        // // Configure the User entity
        // modelBuilder.Entity<User>()
        //     .HasKey(u => u.Id);

        // modelBuilder.Entity<User>()
        //     .Property(u => u.FullName)
        //     .IsRequired()
        //     .HasMaxLength(100);

        // modelBuilder.Entity<User>()
        //     .Property(u => u.Email)
        //     .IsRequired()
        //     .HasMaxLength(100);

        // modelBuilder.Entity<User>()
        //     .Property(u => u.PasswordHash)
        //     .IsRequired();

        // // Configure the NotificationPreference entity
        // modelBuilder.Entity<NotificationPreference>()
        //     .HasKey(np => np.Id);

        // modelBuilder.Entity<NotificationPreference>()
        //     .Property(np => np.Preference)
        //     .IsRequired();
    }
}