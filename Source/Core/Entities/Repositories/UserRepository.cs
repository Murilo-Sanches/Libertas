using Libertas.Source.Core.Entities.Context;
using Libertas.Source.Core.Entities.DAO;
using Libertas.Source.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Libertas.Source.Core.Entities.Repositories;

public class UserRepository(DataContext dataContext) : IUserDAO
{
    private readonly DataContext _ctx = dataContext;

    public async Task<IEnumerable<User>> GetUsersAsync() => await _ctx.Users.ToListAsync();

    public async Task<User?> FindUserByIdAsync(int id) => await _ctx.Users.FindAsync(id);

    public async Task<User> AddUserAsync(User user)
    {
        _ctx.Users.Add(user);
        await _ctx.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _ctx.Users.Update(user);
        await _ctx.SaveChangesAsync();
        return user;
    }

    public async Task RemoveUserAsync(User user)
    {
        _ctx.Users.Remove(user);
        await _ctx.SaveChangesAsync();
    }
}
