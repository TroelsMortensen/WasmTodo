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

    public async Task<Todo> CreateAsync(TodoCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        Todo todo = new Todo(user, dto.Title);
        
        ValidateTodo(todo);
        
        Todo created = await todoDao.CreateAsync(todo);
        return created;
    }

    public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters)
    {
        return todoDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(TodoUpdateDto dto)
    {
        Todo? existing = await todoDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {dto.Id} not found!");
        }

        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }
        
        if (existing.IsCompleted && !dto.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed Todo");
        }

        Todo updated = new Todo(user, dto.Title)
        {
            Id = existing.Id,
            IsCompleted = dto.IsCompleted
        };
        
        ValidateTodo(updated);

        await todoDao.UpdateAsync(updated);
    }

    private void ValidateTodo(Todo dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}