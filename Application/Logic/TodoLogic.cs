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

        Todo todo = new(dto.OwnerId, dto.Title);
        ValidateTodo(todo);
        Todo created = await todoDao.Create(todo);
        return created;
    }
    

    public Task<IEnumerable<Todo>> Get(SearchTodoParametersDto searchParameters)
    {
        return todoDao.Get(searchParameters);
    }

    public async Task Update(Todo todo)
    {
        Todo? existing = await todoDao.GetById(todo.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {todo.Id} not found!");
        }

        User? user = await userDao.GetById(todo.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {todo.OwnerId} was not found.");
        }

        ValidateTodo(todo);

        await todoDao.Update(todo);
    }

    private void ValidateTodo(Todo todo)
    {
        if (string.IsNullOrEmpty(todo.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}