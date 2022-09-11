using Domain.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class TodoFileDao : ITodoDao
{
    private readonly FileContext context;

    public TodoFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Todo> Create(Todo todo)
    {
        int id = 1;
        if (context.Todos.Any())
        {
            id = context.Todos.Max(t => t.Id);
            id++;
        }

        todo.Id = id;

        context.Todos.Add(todo);
        context.SaveChanges();

        return Task.FromResult(todo);
    }

    public Task<IEnumerable<Todo>> Get(SearchTodoParametersDto searchParams)
    {
        IEnumerable<Todo> result = context.Todos.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            User? user = context.Users.FirstOrDefault(u =>
                u.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
            
            if (user != null)
            {
                int ownerId = user.Id;
                result = result.Where(t => t.OwnerId == ownerId);
            }
        }

        if (searchParams.UserId != null)
        {
            result = result.Where(t => t.OwnerId == searchParams.UserId);
        }

        if (searchParams.CompletedStatus != null)
        {
            result = result.Where(t => t.IsCompleted == searchParams.CompletedStatus);
        }

        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task<Todo?> GetById(int todoId)
    {
        Todo? existing = context.Todos.FirstOrDefault(t => t.Id == todoId);
        return Task.FromResult(existing);
    }

    public async Task Update(Todo todo)
    {
        Todo existing = (await GetById(todo.Id))!;
        existing.Title = todo.Title;
        existing.IsCompleted = todo.IsCompleted;
        existing.OwnerId = todo.OwnerId;
        context.SaveChanges();
    }
}