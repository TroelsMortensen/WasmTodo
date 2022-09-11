using Domain.DTOs;
using Domain.Models;

namespace Domain.LogicInterfaces;

public interface ITodoLogic
{
    Task<Todo> CreateAsync(TodoCreationDto dto);
}