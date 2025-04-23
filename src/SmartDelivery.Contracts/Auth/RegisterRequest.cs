namespace SmartDelivery.Contracts.Auth;
using SmartDelivery.Domain.Enums;

public record RegisterRequest(
    string FullName,
    string Email,
    string Password,
    NotificationPreference NotificationPreference
);