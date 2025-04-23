public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
    //Implement UpdateAsync and DeleteAsync methods if needed
}
