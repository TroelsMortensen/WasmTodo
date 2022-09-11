using Domain.DTOs;
using Domain.Models;

namespace Domain.DaoInterfaces;

public interface ITodoDao
{
    Task<Todo> Create(Todo todo);
    Task<IEnumerable<Todo>> Get(SearchTodoParametersDto searchParams);
    Task Update(Todo todo);

    Task<Todo?> GetById(int todoId);
}