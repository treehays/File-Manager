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

    public async Task<ICollection<AttachedDocument>> GetDocumentsByEmailAndTransactionNumberAsync(string email, int transactionNumber)
    {
        var response = await _context.Users.Include(x => x.Transactions).Where(x => x.Email == email).SelectMany(x => x.Transactions).Where(x => x.TransactionNumber == transactionNumber).SelectMany(x => x.AttachedDocuments).ToListAsync();
        return response;
    }

    public async Task SaveChangesAsync()
    {
        var response = await _context.SaveChangesAsync();
    }
}
