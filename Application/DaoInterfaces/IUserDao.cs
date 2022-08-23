using Domain.Models;

namespace Domain.DaoInterfaces;

public interface IUserDao
{
    Task<User> Create(User user);
    Task<User?> GetByUsername(string userName);
}