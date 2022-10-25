using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class TodoEfcDao : ITodoDao
{
    
    private readonly TodoContext context;

    public TodoEfcDao(TodoContext context)
    {
        this.context = context;
    }

    public async Task<Todo> CreateAsync(Todo todo)
    {
        EntityEntry<Todo> added = await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParams)
    {
        IQueryable<Todo> query = context.Todos.Include(todo => todo.Owner).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(todo =>
                todo.Owner.UserName.ToLower().Equals(searchParams.Username.ToLower()));
        }
        
        if (searchParams.UserId != null)
        {
            query = query.Where(t => t.Owner.Id == searchParams.UserId);
        }
        
        if (searchParams.CompletedStatus != null)
        {
            query = query.Where(t => t.IsCompleted == searchParams.CompletedStatus);
        }
        
        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(searchParams.TitleContains.ToLower()));
        }

        List<Todo> result = await query.ToListAsync();
        return result;
    }

    public Task UpdateAsync(Todo todo)
    {
        throw new NotImplementedException();
    }

    public Task<Todo?> GetByIdAsync(int todoId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}