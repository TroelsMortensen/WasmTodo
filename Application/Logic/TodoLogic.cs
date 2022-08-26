using Domain.DaoInterfaces;
using Domain.DTOs;
using Domain.LogicInterfaces;
using Domain.Models;

namespace Domain.Logic;

public class TodoLogic : ITodoLogic
{
    private readonly ITodoDao todoDao;
    private readonly IUserDao userDao;

    public TodoLogic(ITodoDao todoDao, IUserDao userDao)
    {
        this.todoDao = todoDao;
        this.userDao = userDao;
    }

    public async Task<Todo> Create(TodoCreationDto dto)
    {
        User? user = await userDao.GetById(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        ValidateTodo(dto);
        Todo todo = new Todo(dto.OwnerId, dto.Title);
        Todo created = await todoDao.Create(todo);
        return created;
    }

    public Task<IEnumerable<Todo>> Get(SearchTodoParametersDto searchParameters)
    {
        return todoDao.Get(searchParameters);
    }

    private void ValidateTodo(TodoCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}