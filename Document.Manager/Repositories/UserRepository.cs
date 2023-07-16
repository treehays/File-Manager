using System.Linq.Expressions;
using Document.Manager.Gateways.Context;
using Document.Manager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document.Manager.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }


    public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
    {
        var response = await _context.Users.SingleOrDefaultAsync(expression);
        return response;
    }

    public async Task<ICollection<User>> GetListAsync(Expression<Func<User, bool>> expression)
    {
        var response = await _context.Users.Where(expression).ToListAsync();
        return response;
    }

    public async Task SaveChangesAsync()
    {
        var response = await _context.SaveChangesAsync();
    }
}
