using Domain.DTOs;
using Domain.Models;

namespace Domain.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto dto);
}