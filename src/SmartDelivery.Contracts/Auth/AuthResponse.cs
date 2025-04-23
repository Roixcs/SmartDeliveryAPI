public record AuthResponse(
    Guid UserId,
    string FullName,
    string Email,
    string PreferredNotification,
    string Token

    //Implements RefreshToken and RefreshTokenExpiration if needed
);