using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

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

        Todo todo = new Todo(user.Id, dto.Title);

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

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        if (dto.IsCompleted != null && existing.IsCompleted && !(bool)dto.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed Todo");
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        bool completedToUse = dto.IsCompleted ?? existing.IsCompleted;

        Todo updated = new (userToUse.Id, titleToUse)
        {
            IsCompleted = completedToUse,
            Id = existing.Id,
        };

        ValidateTodo(updated);

        await todoDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Todo? todo = await todoDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Todo with ID {id} was not found!");
        }

        if (!todo.IsCompleted)
        {
            throw new Exception("Cannot delete un-completed Todo!");
        }

        await todoDao.DeleteAsync(id);
    }

    public async Task<TodoBasicDto> GetByIdAsync(int id)
    {
        Todo? todo = await todoDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Todo with id {id} not found");
        }

        return new TodoBasicDto(todo.Id, todo.Owner.UserName, todo.Title, todo.IsCompleted);
    }

    private void ValidateTodo(Todo dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}