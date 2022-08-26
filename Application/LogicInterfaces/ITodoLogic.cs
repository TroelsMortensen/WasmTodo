using Domain.DTOs;
using Domain.Models;

namespace Domain.LogicInterfaces;

public interface ITodoLogic
{
    Task<Todo> Create(TodoCreationDto dto);
}