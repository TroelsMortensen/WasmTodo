using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ITodoDao
{
    Task<Todo> CreateAsync(Todo todo);
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParams);
    Task UpdateAsync(Todo todo);
    Task<Todo?> GetByIdAsync(int todoId);
    
    Task DeleteAsync(int id);

}