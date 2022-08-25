using Domain.DTOs;
using Domain.Models;

namespace Domain.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> Create(UserCreationDto dto);
    public Task<IEnumerable<User>> Get(SearchUserParametersDto searchParameters);
}