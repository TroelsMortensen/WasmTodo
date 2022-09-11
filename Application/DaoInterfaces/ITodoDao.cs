using Domain.Models;

namespace Domain.DaoInterfaces;

public interface ITodoDao
{
    Task<Todo> CreateAsync(Todo todo);

}