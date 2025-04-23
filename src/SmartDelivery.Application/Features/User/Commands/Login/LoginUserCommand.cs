public record LoginUserCommand(
    string Email,
    string Password) : IRequest<string>;//Devuelve el JWT    //<LoginUserResponse>;