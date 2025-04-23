public class JwTService : IJwTService
{
    private readonly IConfiguration _configuration;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public JwTService(IConfiguration configuration, IDateTimeProvider dateTimeProvider, IJwtTokenGenerator jwtTokenGenerator)
    {
        _configuration = configuration;
        _dateTimeProvider = dateTimeProvider;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public string GenerateToken(string email)
    {
        var token = _jwtTokenGenerator.GenerateToken(email);
        return token;
    }
}