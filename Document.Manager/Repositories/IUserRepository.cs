using System.Linq.Expressions;
using Document.Manager.Models.Entities;

namespace Document.Manager.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetAsync(Expression<Func<User, bool>> expression);
    Task<ICollection<AttachedDocument>> GetDocumentsByEmailAndTransactionNumberAsync(string email, int transactionNumber);
    Task SaveChangesAsync();
}
