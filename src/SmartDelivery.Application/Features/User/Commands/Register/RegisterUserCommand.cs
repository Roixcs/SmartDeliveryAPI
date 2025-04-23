public record RegisterUserCommand(
    string FullName,
    string Email,
    string Password,
    NotificationPreference NotifiPreference) : IRequest<Guid>;//<RegisterUserResponse>;