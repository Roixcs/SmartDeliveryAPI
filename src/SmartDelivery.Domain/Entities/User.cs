public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public NotificationPreference NotificationPreference { get; set; } = NotificationPreference.Email;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow(-5);
    //public DateTime UpdatedAt { get; set; }
}