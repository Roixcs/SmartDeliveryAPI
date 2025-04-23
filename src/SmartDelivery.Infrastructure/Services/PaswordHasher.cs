public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        // Use a secure hashing algorithm to hash the password
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        // Hash the provided password and compare it with the stored hash
        var hashedProvidedPassword = HashPassword(providedPassword);
        return hashedProvidedPassword == hashedPassword;
    }
}