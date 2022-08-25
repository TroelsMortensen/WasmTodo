using Domain.DTOs;
using Domain.Models;

namespace Domain.DaoInterfaces;

public interface IUserDao
{
    Task<User> Create(User user);
    Task<User?> GetByUsername(string userName);
    public Task<ICollection<User>> Get(SearchUserParametersDto searchParameters);

}