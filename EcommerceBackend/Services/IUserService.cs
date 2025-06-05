using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<User> ValidateUserAsync(string username, string password); // New method
        Task<User> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(User user);
    }
}