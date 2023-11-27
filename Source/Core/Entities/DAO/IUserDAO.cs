using Libertas.Source.Core.Entities.Models;

namespace Libertas.Source.Core.Entities.DAO;

public interface IUserDAO
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> FindUserByIdAsync(int id);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task RemoveUserAsync(User user);
}
