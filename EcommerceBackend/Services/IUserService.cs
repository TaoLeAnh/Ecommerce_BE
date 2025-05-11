using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<User> ValidateUserAsync(string username, string password); // New method
    }
}